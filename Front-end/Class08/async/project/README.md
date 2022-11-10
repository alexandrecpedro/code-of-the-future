# Código do Futuro (Code of the Future)

## 🗂 Modules

### 1 Front-end

#### Class 08 | Angular: Encapsulation and Pipes
    -  View Encapsulation
    -  Lifecycle Hooks
        - Events Order:
            - constructor
            - ngOnChanges = when property-binding value is updated
            - ngOnInit = when component is initialized
            - ngDoCheck = each update verification cycle
                - ngAfterContentInit = after insert external content on view
                - ngAfterContentChecked = each inserted content verification
                - ngAfterViewInit
                - ngAfterViewChecked = each content verification
            - ngOnDestroy = before directives/components be destroyed
    -  Pipes I
    -  Pipes II
    -  Localization: internationalization API (i18n)
    -  Exercise: Pipes I
    -  Exercise: Pipes II
    -  Reorganizing the Project