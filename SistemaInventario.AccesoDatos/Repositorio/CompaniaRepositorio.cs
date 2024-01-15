using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class CompaniaRepositorio : Repositorio<Compania>, ICompaniaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CompaniaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Compania compania)
        {
            var companiaDB = _db.Companias.FirstOrDefault(b => b.Id == compania.Id);
            if(companiaDB != null)
            {
                companiaDB.Nombre = compania.Nombre;
                companiaDB.Descripcion = compania.Descripcion;
                companiaDB.Pais = compania.Pais;
                companiaDB.Ciudad = compania.Ciudad;
                companiaDB.Direccion = compania.Direccion;
                companiaDB.Telefono = compania.Telefono;
                companiaDB.BodegaVentaId = compania.BodegaVentaId;               
                companiaDB.ActualizadoPorId = compania.ActualizadoPorId;
                companiaDB.FechaActualizacion = compania.FechaActualizacion;
                _db.SaveChanges(); //se guardan los datos y actualiza los datos
            }
        }
    }
}
