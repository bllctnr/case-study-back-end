namespace Business.Constants
{
    public class Messages
    {
        //User
        public static string SuccessfulLogin = "Login successful";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string NotAuthorized = "Unauthorized";
        public static string UserAlreadyExists = "User already exist";
        public static string AccessTokenCreated = "Access token created";
        public static string UserRegistered = "User registered succesfully";

        //Record messages
        public static string RecordsAdded = "Records successfully added";
        public static string RecordsListed = "Records successfully listed";
        public static string RecordGetted = "Record successfully getted";
        public static string RecordsUpdated = "Records successfully updated";
        public static string RecordsDeleted = "Records successfully deleted";
        public static string RecordNotFount = "Record not found.";

        //Product messages
        public static string ProductCodeAlreadyExist = "Product code already exist";

        //Order messages
        public static string OutOfStockItemsInOrder = "There are out of stock items in the order.";
    }
}
