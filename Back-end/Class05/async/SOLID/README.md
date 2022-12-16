#### Class 05 | SOLID Principles
    -   Introduction to SOLID
        -   An acronym created by Michael Feathers to validate some of the principles of object-oriented programming and code design identified by Robert C. Martin (Uncle Bob) in the book Clean Code
<br><br>
<table>
    <thead>
        <tr> 
            <th>SOLID Principle</th>
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