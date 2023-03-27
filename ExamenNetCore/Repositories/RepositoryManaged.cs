using ExamenNetCore.Data;
using ExamenNetCore.Helpers;
using ExamenNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenNetCore.Repositories
{
    public class RepositoryManaged
    {
        private ZapatillasContext context;

        public RepositoryManaged(ZapatillasContext context)
        {
            this.context = context;
        }

        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }

        public async Task RegisterUserAsync
            (string nombre, string apellidos, string email, string password, string imagen)
        {
            Usuario usuario = new Usuario();

            int maximo = this.GetMaxIdUsuario();

            usuario.IdUsuario = maximo;
            usuario.Nombre = nombre;
            usuario.Apellidos = apellidos;
            usuario.Email = email;
            usuario.Salt =
                HelperCryptography.GenerateSalt();
            usuario.Password =
                HelperCryptography.EncryptPassword(password, usuario.Salt);
            usuario.Imagen = imagen;

            this.context.Usuarios.Add(usuario);

            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> FindEmailAsync
            (string email)
        {
            Usuario user =
                await this.context.Usuarios.FirstOrDefaultAsync
                (x => x.Email == email);
            return user;
        }

        public async Task<Usuario> ExisteUsuario
            (string email, string password)
        {
            Usuario user = await this.FindEmailAsync(email);
            var usuario = await this.context.Usuarios.Where
                (x => x.Email == email && x.Password ==
                HelperCryptography.EncryptPassword(password, user.Salt)).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
