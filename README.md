# Rocket-Elevators-Rest-API

This repo serves as the Rocket Elevators REST API developed for CodeBoxx's week 8 of the Odyssey program. We were tasked with developing a REST API to interact with the MYSQL database that already exists, and provide the appropriate requests for queries.

The queries for the REST API are found in a public PostMan workspace at: https://app.getpostman.com/join-team?invite_code=f09613b7a24e69fef5524a5d3f5f434e&ws=1c76f8ec-a244-4f00-9317-ee95973e1306

The REST API URL is: https://week-8-restapi-apibehavioroptions-kaelenburroughs.azurewebsites.net/

Each request works as follows:

1. GET Batteries - Returns the information for a specific battery, and different batteries can be returned by changing the number at the end of the API request.

2. GET Batteries Status - Returns the current status of the queried battery.

3. PUT Batteries Status - Changes the status of the queried battery to either 'Active', 'Inactive', or 'Intervention'.

4. GET Columns - Returns the information for a specific column, and different columns can be returned by changing the number at the end of the API request.

5. GET Columns Status - Returns the current status of the queried column.

6. PUT Columns Status - Changes the status of the queried column to either 'Active', 'Inactive', or 'Intervention'.

7. GET Elevators - Returns the information for a specific elevator, and different elevators can be returned by changing the number at the end of the API request.

8. GET Elevators Status - Returns the current status of the queried elevator.

9. PUT Elevators Status - Changes the status of the queried elevator to either 'Active', 'Inactive', or 'Intervention'.

10. GET Elevators List Not Active - Returns a list of elevators that do not have a status of 'Active'.

11. GET Buildings List Intervention - Returns a list of all the buildings that have a battery, column, or elevator with a status of 'Intervention'.

12. Get Leads (all) - Returns a list of all the leads in the database.

13. GET Leads (Last 30 Days) - Returns a list of all leads submitted in the last 30 days, where the submitted lead is not also linked to a customer.

WEEK 9 - Consolidation

This week, we had to add new endpoints for the interventions created in the rails app, and they can be found at the PostMan team workspace here, https://app.getpostman.com/join-team?invite_code=f09613b7a24e69fef5524a5d3f5f434e&ws=42edd766-bbd4-4fab-985f-6b3d71ef8b9d, and are all as follows:

1. GET Interventions (all) - Shows all interventions in the database.

2. GET Intervention - Returns the information for a specific intervention, and different interventions can be returned by changing the number at the end of the API request. 

3. GET Interventions (No start date, Pending) - Returns all interventions without a start date and a status of "Pending".

4. PUT Change to "In Progress" - Changes the status of the specified intervention to "In Progress" and sets the start date to the time that the request was sent.

5. PUT Change to "Completed" - Changes the status of the specified intervention to "Completed" and sets the end date to the time that the request was sent.

The URL for the API is the same as the one mentioned earlier in this README.
