#### Class 09 | MVC Pattern, Models and Views
    -   MVC Pattern: WebForms vs MVC
        -   Programming paradigms
<br><br>
<table>
    <thead>
        <tr> 
            <th scope="col" colspan="2">WEB FORMS</th>
            <th scope="col" colspan="2">ASP.NET MVC</th>
        </tr>
    </thead>
    <tbody>
        <tr> 
            <td>
                <ul>
                    <li>BROWSER</li>
                    <li>IIS (Internet Information Services)</li>
                    <li>ASP.NET HTTP RUNTIME</li>
                    <li>PAGE HTTP HANDLER</li>
                    <li>PAGE CLASS</li>
                    <li>PAGE LIFECYCLE (PRELIMINARIES)</li>
                    <li>POSTBACK EVENT</li>
                    <li>PAGE LIFECYCLE (FINALIZATION)</li>
                    <li>UPDATING CONTROLS</li>
                    <li>RESPONSE OUTPUT STREAM</li>
                </ul>
            </td>
            <td>
                <ul>
                    <li>User access the application</li>
                    <li>Software hosting server</li>
                    <li>It is used to extend ASP.NET so we can add, remove, and adapt/extend functionalities as needed<wbr>It will call WebForms</li>
                    <li>It processes requests for an endpoint (url), which will redirect user for WebForms initial page</li>
                    <li>First part of WebForms (starting). The url address is the page name</li>
                    <li>Each page has a lifecycle. It starts with the first request<wbr>At that moment (first request), ASP.NET will find if it has already been compiled, and/or analyzed,<wbr>to assemble its structure in memory</li>
                    <li>This event always happens when a event that changes the page occurs<wbr>(user clicks on button, confirms an action), and the user doesn't get out of the page<wbr>The page is reloaded and the ASP.NET with WebForms treats all requests as if an user had a browsing history<wbr>The fields are filled in as an user navigates through the page<wbr>This happens hidden from the developer, so that the user has a similar experience with Windows Forms</li>
                    <li>The page is discarded, freed from memory</li>
                    <li>A cleanup is done as soon as the page is no longer needed</li>
                    <li>A response will be delivered as a stream</li>
                </ul>
            </td>
            <td>
                <ul>
                    <li>BROWSER</li>
                    <li>IIS (Internet Information Services)</li>
                    <li>ASP.NET HTTP RUNTIME</li>
                    <li>URL ROUTER</li>
                    <li>MVC HTTP HANDLER</li>
                    <li>CONTROLLER FACTORY</li>
                    <li>METHOD EXECUTION</li>
                    <li>VIEW ENGINE</li>
                    <li>RESPONSE OUTPUT STREAM</li>
                </ul>
            </td>
            <td>
                <ul>
                    <li>User access the application</li>
                    <li>Software hosting server</li>
                    <li>It is used to extend ASP.NET so we can add, remove, and adapt/extend functionalities as needed<br>It will call WebForms</li>
                    <li>The router tells us on which resource/class that route will be executed<wbr>That strategy can be used to separate by folders, product code, client code, etc<wbr>A logic is created in the route to distribute access according to the structure of the application</li>
                    <li>The router will deliver to the MVC framework through a handler<wbr>This handler will deliver the route to a controller factory</li>
                    <li>It is the route management, which calls an specific controller, who execute a method</li>
                    <li>Known as an action. Each controller has one or more actions (insert, find, update, delete)</li>
                    <li>After executing an action, there will be a response (string, number, ..., or even a webpage)<wbr>In case of a webpage, unlike the WebForms page, there will be rendered a view (page engine)<wbr>In MVC that view engine is used with the Razor (motor)<wbr>In addition to the view engine, raw data is needed to render a page</li>
                    <li>A response will be delivered as a stream</li>
                </ul>
            </td>
        </tr>
    </tbody>
</table>
<br><br>

        ```
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

                ```c#
                    routes.MapMvcAttributeRoutes();
                ```
            
            -   Route Restrictions
    -   MVC Pattern: Action Result
<br><br>
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
<br><br>

        ```
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