# Dynamic-Form
A custom directive built using Angular 1.6.7 which lets users create an entire Form element using just this one directive. The project was developed during an internship at Ernst and Young so the databases and connection strings are theirs.
Technologies Used: 
 * Visual Studio 2013
 * ASP.net Web API
 * AngularJS
 * HTML, CSS, Bootstrap
 * SQL server manager studio 2012
 
The flow goes like following:
User specifies in the database which fields he requires for the form and what type the are. After specifying he just has to run the api and include dynamic-form.js, app.js, ui-grid.js, ui-grid.css and style.css in his project and the reference to js and css files in his html file where he is creating the form. Use the <dynamic-form> directive and boom the form is ready. Also not just the front end, even the backend is handled by the api. User just mentions the Stored Procedure he wants to execute in the page description table and the "GET RESULT" button does the rest for him neatly showing the output of the form in a grid which is enabled with sorting, hiding and manipulating the columns.
