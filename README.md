```
ssh deploy@your-droplet deploy-plat-levelworks.sh
```

## Technology Stack

### Server
The backend segment of the application is crafted using ASP.NET Core, incorporating the Clean Architecture as its core design principle. Employing this foundational design pattern ensures a well-organized and maintainable structure. Database access is orchestrated through the repository pattern, leveraging Entity Framework Core as the underlying tool to interact seamlessly with the database.

For the smooth operation of the server, it is imperative to install Dotnet Runtime 7.0. This prerequisite ensures that the server component runs effectively, utilizing the latest runtime environment. The combination of ASP.NET Core, Clean Architecture, and Entity Framework Core establishes a robust foundation for the server-side of the application, promoting scalability, maintainability, and efficient database interactions.

### Client
The client component of the application is developed using React, adhering to a well-organized and modular design philosophy. React serves as the backbone for efficiently managing user interactions and dynamic interfaces, delivering a responsive and immersive user experience. Within the application, React components are strategically employed to encapsulate specific functionalities, fostering code reusability and maintainability.

To establish seamless communication with the server, the client interfaces with the ASP.NET Core Web API. This integration follows a clean and well-defined interface, promoting a clear separation of concerns between the client and server elements. By adopting this approach, the client-side architecture ensures a robust and scalable foundation, enhancing the overall performance and maintainability of the application.

## How to Run
+ Ensure you have npm installed for the React frontend.
+ Install Dotnet Runtime 9.0 for the ASP.NET Core backend.
+ Clone the repository and navigate to the project directory.
+ Run npm install to install frontend dependencies.
+ Run dotnet build and dotnet run to launch the application.

```shell
// Clone repository
git clone https://github.com/Bazl1/Bloggy.git
cd ./bloggy

// Server launch
cd ./server/bloggy
dotnet run --project ./Bloggy.Api/

// Client launch
cd ./client
npm run dev
```
