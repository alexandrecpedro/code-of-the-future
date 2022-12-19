#### Class 06 | Best pratices for software development and testing
    -   Introduction to Best Practices
        -   Clean Code - informal methodology
            -   Definition
                -   Clean Code is a development philosophy whose main objective is to apply simple techniques 
                that aim to facilitate the writing and reading of a code.
            -   Reasons to write clean code
                -   Reading is harder than writing
                -   Technical deficit is terrible
                -   Don't be lazy (There is a "good lazy" = the one related to rework)
                -   Careless code equals slow code
                -   Don't be the bad code reference
<table>
    <thead>
        <tr> 
            <th>Principle</th>
            <th>Robert C. Martin citation</th> 
            <th>Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr> 
            <td>SPR - Single Responsibility Principle</td>
            <td>"A class should have one, and only one, reason to change."</td> 
            <td>This principle states that each module or class should have responsibility for a single part of the functionality provided by the software</td>
        </tr>
        <tr> 
            <td>OCP - Open-Closed Principle</td>
            <td>"You should be able to extend a classes behavior, without modifying it."</td> 
            <td>Software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification</td>
        </tr>
        <tr> 
            <td>LSP - Liskov Substitution Principle</td>
            <td>"Derived classes must be substitutable for their base classes."</td> 
            <td>Simplifying, if class A is a subtype of class B, we can replace B with A without breaking our program's behavior</td>
        </tr>
        <tr> 
            <td>ISP - Interface Segregation Principle</td>
            <td>"Make fine grained interfaces that are client specific."</td> 
            <td>Larger interfaces must be split into smaller ones. By doing this, we can ensure that the implementing classes only have to worry about the methods that are their interest</td>
        </tr>
        <tr> 
            <td>DIP - Dependency Inversion Principle</td>
            <td>"Depend on abstractions, not on concretions."</td> 
            <td>The principle of dependency inversion refers to the decoupling of software modules. That way, instead of high-level modules relying on low-level modules, both will rely on abstractions</td>
        </tr>
    </tbody>
</table>
```shell
    -   Clean Code Principles
```
<table>
    <thead>
        <tr> 
            <th>Principle</th>
            <th>Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr> 
            <td>Select the correct tool</td>
            <td>No tool / design pattern is ideal for everything <br>Don't mix tools of diferent responsibilities. Keep as native as possible. E.g.: don't mix HTML at C# or JavaScript at HTML (on a simple page without framework for example)</td>
        </tr>
        <tr> 
            <td>DRY - Don't Repeat Yourself</td>
            <td>
                We must write a piece of knowledge or logic only once <br>Make your code as reusable as possible <br>Problems with code duplication:
                <ul>
                    <li>Reduces code clarity</li>
                    <li>Increases code lines for debug and maintenance</li>
                    <li>It doen't mean that everytime we couldn't write similar logic to another part of the system</li>
                </ul>
            </td>
        </tr>
        <tr> 
            <td>Make a self-documenting code</td>
            <td>
                Write code so clear that the author's intent is obvious to the point where a novice reads the code and understands it instantly
                <ul>
                    <li>Formatting and indentation are important</li>
                    <li>Prefer code over comments</li>
                </ul>
            </td>
        </tr>
    </tbody>
</table>
```shell
    -   Clean Code: Classes Name
        -   Choose class Name guide:
            -   It must be nouns not verbs
            -   Be specific
                -   Cohesive class => each method interacts with the others or with the class properties
            -   Avoid generic suffixes or prefixes
    -   Clean Code: Methods Name
    -   Clean Code: Function Creation
        -   1 feature per function
        -   Completed logic
        -   Reasons to create a function
        <ol>
            <li>Avoid code duplication</li>
            <li>Code indentation</li>
            <li>When you have a not clear piece of code, the fuction serves as a summary / an introduction to the subject</li>
            <li>Since the function should only have a single responsibility, it shouldn't be too long</li>
        <ol>
    -   Clean Code: Code Sample
    -   Clean Code: Class Creation
        -   Reasons to create a class
        <ol>
            <li>Model a real-life object</li>
            <li>Class methods have little or nothing to do with each other (Low cohesion class)</li>
            <li>Allow code reuse</li>
            <li>Reduce the complexity</li>
            <li>Clear parameters quantity (reduces)</li>
            <li>Class that changes a lot on any commit</li>
        </ol>
        -   Signals of very small classes (avaliate if two smaller classes should be only one)
        <ol>
            <li>Classes that heavily depend on each other (the same business rule)</li>
            <li>Class that always uses one feature of the other</li>
            <li>Class so small that it is difficult to understand how the system works</li>
        </ol>
```