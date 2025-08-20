using ErrorOr;

namespace ToDoList.Domain.DomainErrors
{
    public static partial class Errors
    {
        public static class TodoTask
        {
            public static Error NotFound =>
                Error.NotFound("Task.NotFound", "La tarea no se encontró.");
        }

        public static class User
        {
            public static Error NotFound =>
                Error.NotFound("User.NotFound", "El usuario no existe.");

            public static Error InvalidCredentials =>
                Error.Validation("User.InvalidCredentials", "El usuario o password no son válidos.");

            public static Error EmailAlreadyExists =>
                Error.Conflict("User.EmailAlreadyExists", "El email ya existe.");

            public static Error InvalidPassword =>
                Error.Validation("User.InvalidPassword", "El password no cumple con los criterios establecidos.");
        }
    }
}