#### Class 09 | MVC Pattern, Models and Views
    -   MVC Pattern: WebForms vs MVC
        -   Programming paradigms
<table>
    <thead>
        <tr> 
            <th scope="col" colspan="2">WEB FORMS</th>
            <th scope="col" colspan="2">ASP.NET MVC</th>
        </tr>
    </thead>
    <tbody scope="row" rowspan="2">
        <tr colspan="11">
            <td>BROWSER</td>
            <td>User access the application</td>
            <td>BROWSER</td>
            <td>User access the application</td>
        </tr>
        <tr>
            <td>IIS (Internet Information Services)</td>
            <td>Software hosting server</td>
            <td>IIS (Internet Information Services)</td>
            <td>Software hosting server</td>
        </tr>
        <tr>
            <td>ASP.NET HTTP RUNTIME</td>
            <td>It is used to extend ASP.NET so we can add, remove, and adapt/extend functionalities as needed. It will call WebForms</td>
            <td>ASP.NET HTTP RUNTIME</td>
            <td>It is used to extend ASP.NET so we can add, remove, and adapt/extend functionalities as needed. It will call URL ROUTER</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>URL ROUTER</td>
            <td>The router tells us on which resource/class that route will be executed. That strategy can be used to separate by folders, product code, client code, etc. A logic is created in the route to distribute access according to the structure of the application</td>
        </tr>
        <tr>
            <td>PAGE HTTP HANDLER</td>
            <td>It processes requests for an endpoint (url), which will redirect user for WebForms initial page</td>
            <td>MVC HTTP HANDLER</td>
            <td>The router will deliver to the MVC framework through a handler. This handler will deliver the route to a controller factory</td>
        </tr>
        <tr>
            <td>PAGE CLASS</td>
            <td>First part of WebForms (starting). The url address is the page name</td>
            <td>CONTROLLER FACTORY</td>
            <td>It is the route management, which calls an specific controller, who execute a method</td>
        </tr>
        <tr>
            <td>PAGE LIFECYCLE (PRELIMINARIES)</td>
            <td>Each page has a lifecycle. It starts with the first request. At that moment (first request), ASP.NET will find if it has already been compiled, and/or analyzed, to assemble its structure in memory</td>
            <td>METHOD EXECUTION</td>
            <td>Known as an action. Each controller has one or more actions (insert, find, update, delete)</td>
        </tr>
        <tr>
            <td>POSTBACK EVENT</td>
            <td>This event always happens when a event that changes the page occurs (user clicks on button, confirms an action), and the user doesn't get out of the page. The page is reloaded and the ASP.NET with WebForms treats all requests as if an user had a browsing history. The fields are filled in as an user navigates through the page. This happens hidden from the developer, so that the user has a similar experience with Windows Forms</td>
            <td>VIEW ENGINE</td>
            <td>After executing an action, there will be a response (string, number, ..., or even a webpage. In case of a webpage, unlike the WebForms page, there will be rendered a view (page engine). In MVC that view engine is used with the Razor (motor). In addition to the view engine, raw data is needed to render a page</td>
        </tr>
        <tr>
            <td>PAGE LIFECYCLE (FINALIZATION)</td>
            <td>The page is discarded, freed from memory</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>UPDATING CONTROLS</td>
            <td>A cleanup is done as soon as the page is no longer needed</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>RESPONSE OUTPUT STREAM</td>
            <td>A response will be delivered as a stream</td>
            <td>RESPONSE OUTPUT STREAM</td>
            <td>A response will be delivered as a stream</td>
        </tr>
    </tbody>
</table>

```
    -   MVC Pattern: Introduction to MVC Pattern
        -   Why MVC?
            -   MVC facilitates the development of complex applications
            -   Focuses on separation of responsibilities
            -   The aim is to separate content from presentation and data processing from content
        -   Differences from WebForms
            -   It is a lightweight framework, highly testable compared to the old WebForms
            -   Separates view from code (fully separated entity View)
            -   There is no request for a specific page 
                -   The requests go for controller which redirects according to the implemented logic
                -   Find the data at the model layer
                -   Then redirects this data to the view
                -   The view, in its turn,is then used to display the data to the end user
        - Responsabilities
            -   Model
                -   Represents the app status and the business/operation logic that should be performed
                -   The business logic must be encapsulated along with any application logic to persist application state
            -   View
                -   Must present the content through the user interface
                -   Typically, it will be handled through a mechanism called Razor (view engine),
                    which will interpret and insert the .NET code within an HTML to be able to generate 
                    the final result (page displayed to the user)
                -   To o simplify the view (avoid very large logic), you can extract small components
                    (display components), allowing the reuse of these components in other places of the application
            -   Controller
                -   Manages que the other components (Model and View)
                -   Takes care of user interaction
                    -   Takes input from the user request
                    -   Processes it
                    -   Returns a response to the user page
                -   Initial entry point and is responsible for selecting:
                    -   which model types will be used to perform the task
                    -   which view will be rendered on the page
    -   MVC Pattern: Routing
        -   Route Configuration
            -   Global.asax.cs
                -   Method Application_Start() => register routes
                    -   App_Start => folder
                        -   RouteConfig.cs => file that creates default route table
    -   MVC Pattern: Attribute Routing
        -   Used for specific rules
        -   To enable attribute routing
            -   Go to App_Start/RouteConfig.cs
            -   Add the following lines
                <script>
                    routes.MapMvcAttributeRoutes();
                </script>
            -   Route Restrictions
    -   MVC Pattern: Action Result
```
<table style="display: flex; justify-content: center; align-items: center; width: 50%;">
    <thead>
        <tr>
            <th>Action Result</th>
            <th>Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>ViewResult</td>
            <td>represents HTML</td>
        </tr>
        <tr>
            <td>EmptyResult</td>
            <td>it does not represent any result</td>
        </tr>
        <tr>
            <td>RedirectResult</td>
            <td>redirect user to other URL</td>
        </tr>
        <tr>
            <td>JsonResult</td>
            <td>object in JSON notation</td>
        </tr>
        <tr>
            <td>ContentResult</td>
            <td>text result</td>
        </tr>
        <tr>
            <td>FileContentResult</td>
            <td>file to download</td>
        </tr>
    </tbody>
</table>

```
    -   MVC Pattern: Views
    -   Models: Models Creation
    -   Models: Input I
    -   Models: Input II
    -   Models: HTTP Verbs
    -   Models: Model Validation I
    -   Models: Model Validation II
    -   Views: Partial Views
    -   Views: Layouts