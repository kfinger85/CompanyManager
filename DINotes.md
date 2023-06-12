## Why Use Interfaces?
You should use `IWhatever` as the type for any dependencies in your classes. This allows you to easily swap out the `whatever` service in the future without needing to change your classes.

### Decoupling: 
By depending on an interface (also known as a contract) rather than a concrete implementation, your classes become more decoupled. 
This means that as long as the contract doesn't change, you can swap out the concrete implementation without affecting the classes that use it.

### Testability: 
Interfaces make it easier to unit test your classes, as you can mock the interfaces that your class depends on. This allows you to isolate the class you are testing.

### Flexibility: 
Having an interface allows you to have multiple implementations. For example, you might have InMemoryEmailService for testing and SmtpEmailService for production.


`services.AddScoped<IEmailService, EmailService>();`

You're telling the .NET Core dependency injection system: "Whenever someone asks for an IEmailService, give them an instance of EmailService, and create a new instance for each unique request scope (i.e., each HTTP request)."

In your controllers, or wherever you use the service, you would use constructor injection to get an instance of IEmailService: