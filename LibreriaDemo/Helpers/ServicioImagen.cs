using Firebase.Auth;
using Firebase.Storage;

namespace LibreriaDemo.Helpers
{
    public class ServicioImagen : IServicioImagen
    {
        public async Task<string> SubirImagen(Stream archivo, string nombre)
        {
            string email = "tecnologershn@gmail.com";
            string clave = "Tecno123.";
            string ruta = "peliculasdemo.appspot.com";
            string api_key = "AIzaSyChhPnzaVrpRraIo1JO5esK_mzo7gb4Ruk";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
              ruta,
              new FirebaseStorageOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                  ThrowOnCancel = true
              })

               .Child("Fotos_Perfil")
               .Child(nombre)
               .PutAsync(archivo, cancellation.Token);

            var downloadURL = await task;
            return downloadURL;

        }
    }
}
