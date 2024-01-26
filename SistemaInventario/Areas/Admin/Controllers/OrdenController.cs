using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewModels;
using SistemaInventario.Utilidades;
using System.Security.Claims;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdenController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        [BindProperty]
        public OrdenDetalleVM ordenDetalleVM { get; set; }  

        public OrdenController(IUnidadTrabajo unidadTrabajo)
        {
                _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detalle(int id)
        {
            ordenDetalleVM = new OrdenDetalleVM()
            {
                Orden = await _unidadTrabajo.Orden.ObtenerPrimero(o=>o.Id == id, incluirPropiedades: "UsuarioAplicacion"), //Traemos orden que el usuario esta seleccionando
                OrdenDetalleLista = await _unidadTrabajo.OrdenDetalle.ObtenerTodos(d=>d.OrdenId == id, incluirPropiedades: "Producto")//Traer el detalle de una orden especifica(d=>d.OrdenId == id)
            };
            return View(ordenDetalleVM);
        }

        [Authorize(Roles =DS.Rol_Admin)]
        public async Task<IActionResult> Procesar(int id)
        {
            var orden = await _unidadTrabajo.Orden.ObtenerPrimero(o => o.Id == id);
            orden.EstadoOrden = DS.EstadoEnProceso;
            await _unidadTrabajo.Guardar();
            TempData[DS.Exitosa] = "Orden cambiada a estado en proceso";
            return RedirectToAction("Detalle", new {id = id});

        }

        [HttpPost]
        [Authorize(Roles = DS.Rol_Admin)]
        public async Task<IActionResult> EnviarOrden(OrdenDetalleVM ordenDetalleVM)
        {
            var orden = await _unidadTrabajo.Orden.ObtenerPrimero(o => o.Id == ordenDetalleVM.Orden.Id);
            orden.EstadoOrden = DS.EstadoEnviado;
            orden.Carrier = ordenDetalleVM.Orden.Carrier;
            orden.NumeroEnvio = ordenDetalleVM.Orden.NumeroEnvio;
            orden.FechaEnvio = DateTime.Now;
            await _unidadTrabajo.Guardar();
            TempData[DS.Exitosa] = "Orden cambiada a estado Enviado";
            return RedirectToAction("Detalle", new { id = ordenDetalleVM.Orden.Id });

        }

        //Metodo Api
        #region
        [HttpGet]
        public async Task<IActionResult> ObtenerOrdenLista (string estado)
        {
            var claimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = claimIdentidad.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Orden> todos; //Declaramos la variable
            if(User.IsInRole(DS.Rol_Admin)) // Validar el Rol del Usuario - si el rol del usuario es un rol administrador(DS.Rol_Admin)
            {
                todos = await _unidadTrabajo.Orden.ObtenerTodos(incluirPropiedades: "UsuarioAplicacion");
            }
            else //caso contrario, si el rol no es administrador solo podra ver sus propias ordenes(o=>o.UsuarioAplicacionId == claim.Value)
            {
                todos = await _unidadTrabajo.Orden.ObtenerTodos(o=>o.UsuarioAplicacionId == claim.Value,incluirPropiedades: "UsuarioAplicacion");
            }
            //Validar el estado
            switch (estado)
            {
                case "aprobado":
                    todos = todos.Where(o=>o.EstadoOrden == DS.EstadoAprobado);//filtramos
                    break;
                case "completado":
                    todos = todos.Where(o=>o.EstadoOrden == DS.EstadoEnviado); 
                    break;   
                default:
                    break;
            }
            //Lo enviamos de tipo Json
            return Json(new {data = todos}); 
        }


        #endregion
    }
}
