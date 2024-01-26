using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{
    public static class DS
    {
        public const string Exitosa = "Exitosa";
        public const string Error = "Error";

        public const string ImagenRuta = @"\imagenes\producto\";
        public const string ssCarroCompras = "Sesión Carro Compras";

        public const string Rol_Admin = "Admin";
        public const string Rol_Cliente = "Cliente";
        public const string Rol_Inventario = "Inventario";

        //Estados de la orden

        public const string EstadoPendiente = "Pendiente";
        public const string EstadoAprobado = "Aprobado";
        public const string EstadoEnProceso = "Procesando";
        public const string EstadoEnviado = "Enviado";
        public const string EstadoCancelado = "Cancelado";
        public const string EstadoDevuelto = "Devuelto";

        //Estado del pago de la orden

        public const string PagoEstadoPendiente = "Pendiente";
        public const string PagoEstadoAprobado = "Aprobado";
        public const string PagoEstadoRetrazado = "Retrazado";
        public const string PagoEstadoRechazado = "Rechazado";
        
    }
}
