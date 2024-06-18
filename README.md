# Phone Directory Microservices

## Setup and Run

1. Install MongoDB and configure connection strings in `appsettings.json`.
2. Start the Kafka server.
3. Start the PersonService and ReportService projects:
    ```bash
    dotnet run --project PersonService
    dotnet run --project ReportService
    ```
4. Start the Unit Test projects:
    ```bash
    dotnet test
    ```

## API Endpoints

### PersonService
- `GET /api/persons`: List all persons.
- `GET /api/persons/{id}`: Get a person by ID.
- `POST /api/persons`: Create a new person.
- `DELETE /api/persons/{id}`: Delete a person by ID.
- `POST /api/persons/{id}/contactinfo`: Add contact information to a person.
- `DELETE /api/persons/contactinfo/{id}`: Delete contact information by ID.
- `GET /api/contactinfo`: List all contactinfo.
- `GET /api/contactinfo/{id}`: Get a contactinfo by ID.

### ReportService
- `GET /api/reports`: List all reports.
- `GET /api/reports/{id}`: Get a report by ID.
- `POST /api/reports`: Request a new report.
- `DELETE /api/reports`: Request a new report.