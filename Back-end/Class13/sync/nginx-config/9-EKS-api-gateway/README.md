Utilizando ekclt
- Instalando
 - https://docs.aws.amazon.com/eks/latest/userguide/eksctl.html
```shell
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
brew tap weaveworks/tap
brew install weaveworks/tap/eksctl
brew upgrade eksctl && brew link --overwrite eksctl
eksctl version
```

Criando cluster
- https://docs.aws.amazon.com/eks/latest/userguide/getting-started-eksctl.html
```shell
eksctl create cluster --name k8s-desafio --region us-east-1
kubectl get nodes -o wide
kubectl get pods -A -o wide
```

Apagar Cluster
- https://docs.aws.amazon.com/eks/latest/userguide/getting-started-eksctl.html
```shell
eksctl delete cluster --name k8s-desafio --region us-east-1
```

Utilizando console awscli
- https://docs.aws.amazon.com/cli/latest/reference/eks/create-cluster.html

Criando cluster
```shell
aws eks create-cluster --name --region us-east-1 desafio-k8s --role-arn arn:aws:iam::763818760783:role/eksClusterRole2 --resources-vpc-config subnetIds=subnet-ef5b8fa6,subnet-1fa24744,subnet-df562fba,securityGroupIds=sg-0bfc78ff0cd8590be

rm -rf ~/.kube/
aws eks update-kubeconfig --name desafio-k8s --region us-east-1
```

Apagar Cluster
```shell
aws eks delete-cluster --name desafio-k8s
```

Criar os worknodes
- Entrar no cloud formation
- https://us-east-1.console.aws.amazon.com/cloudformation/home?region=us-east-1#/stacks/create/template

Colocar o script 
- Amazon S3 URL:
- https://amazon-eks.s3-us-west-2.amazonaws.com/cloudformation/2018-12-10/amazon-eks-nodegroup.yaml

Nome Cloud Formation: 
- k8s-ilab-nodes

Nome cluster
- desafio-k8s

ClusterControlPlaneSecurityGroup
 - Selecione o criado pelo EKS

NodeGroupName
- < UM NOME QUE VC QUEIRA >

NodeImageId
- https://cloud-images.ubuntu.com/docs/aws/eks/
- ami-00ff481e776f6a0c2

KeyName
- < SUA CHAVE SSH CADASTRADA NA AWS >

Selecione a rede
- VPC
- subnets

Nós criados agora é somente ir no seu console e atrelar os nodes ao seu k8s
```shell
wget https://amazon-eks.s3-us-west-2.amazonaws.com/cloudformation/2018-08-30/aws-auth-cm.yaml
```
No cloudformation criado ir em outputs e copiar o NodeInstanceRole:
- exemplo: arn:aws:iam::473247640396:role/k8s-ilab-nodes-NodeInstanceRole-126RT2GZ42G5X
- https://us-east-1.console.aws.amazon.com/cloudformation/

Depois colar no arquivo baixado:
```yml
apiVersion: v1
kind: ConfigMap
metadata:
  name: aws-auth
  namespace: kube-system
data:
  mapRoles: |
    - rolearn: arn:aws:iam::xxxxxx:role/k8s-nodes-NodeInstanceRole-xxxxxxxxxx
      username: system:node:{{EC2PrivateDNSName}}
      groups:
        - system:bootstrappers
        - system:node
```
  
Depois rodar no terminal 
```shell
kubectl apply -f aws-auth-cm.yaml