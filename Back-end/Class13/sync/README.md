#### Class 13 | Deploy application
    -  Deploy application
        -   Manual (Hands on)
            -   Azure
                -   Subscription: Pay per use
                    -   Resource group: Code of the Future
                -   VM name: dotnet
                -   Region: (US) Central US
                -   Availability options: No infrastructure redundance required
                -   Security type: Standard
                -   Image: Ubuntu Server 20.04 LTS
                -   Select VM size: vCPUs 2 4GM RAM
                -   Cost: R$ 199,86 / month
                -   Key pair: Azure creates and you can download
                    -   Allow port SSH (22)
                -   Network: default
                -   Number of instances: 1
```
    After create an instance, choose Connect and follow the instructions
    Install internet server (Nginx, Apache, IIS, etc)
    Networking tab => change Inbound port rules (add an specific port, to modify firewall)
    Install MySQL-Server and change permission to public
        - Create an user
            CREATE USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'root';
            GRANT ALL PRIVILEGES ON *.* TO 'root'@'%';
            FLUSH PRIVILEGES;
        - Verify
            SELECT user,authentication_string,plugin,host FROM mysql.user;
        - Give external access
```
            -   AWS
                -   EC2
                    -   VM name: dotnet
                    -   Region: US East - Northern Virginia (us-east-1)
                    -   OS: Linux (Ubuntu)
                    -   Instance: vCPUs 2 4GB RAM
                    -   Cost: R$ 181,39 / month
                    -   Key pair: AWS creates and you can download
                    -   Network: default
                    -   Configure Storage: 1x8 GIB gp2
                    -   Number of instances: 1
```
    After create an instance, choose Connect, then SSH tab
    Install internet server (Nginx, Apache, IIS, etc)
    Security tab => change Inbound rules (add an specific port)
```
        -   Automatizated (IaC)
            -   Terraform
            -   Ansible
    -