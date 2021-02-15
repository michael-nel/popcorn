namespace Domain.Resources
{
    public static class Resource
    {

        public static string RequestNotbeNull = "Request não pode ser nulo";
        
        public static string EmailIsInvalid = "Email inválido";
        public static string DurationIsInvalid = "Duração inválida";
        public static string ImageIsRequired = "Image é requerida";
        public static string TicketIsInvalid = "Ticket valor é inválido";
        public static string SessionEndIsInvalid = "Sessão Final é inválida";

        public static string PasswordMustBeBetween3and32 = "Password inválido deve conter de 3 a 32 caracteres";
        public static string FirstNameMustBeBetween3And150 = "Primeiro nome deve conter entre 3 e 150 caracteres";
        public static string LastNameMustBeBetween3And150 = "Último nome deve conter entre 3 e 150 caracteres";
        public static string TitleMustBeBetween3And250 = "Titulo deve conter entre 3 e 250 caracteres";
        public static string DescriptionMusteBeBetween3And500 = "Descrição deve conter entre 3 e 500 caracteres";


        public static string EmailExists = "Email já cadastrado";
        public static string TitleExists = "Titulo já cadastrado";
        public static string MovieExistsInSession = "Existe uma seção vinculada ao filme";
        public static string ExisteRoominAnotherSession = "Sala está sendo ocupada por outra seção";
        public static string SessionCanNotBeDeleteUnder10Days = "Sessão não pode ser removida, inferior a 10 dias";

        public static string UserNotFound = "Usuário não encontrado";
        public static string MovieNotFound = "Filme não encotrado";
        public static string SessionNotFound = "Session não encontrada";
    }
}
