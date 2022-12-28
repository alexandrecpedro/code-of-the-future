CREATE TABLE IF NOT EXISTS `crud_produtos`.`produtos` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `nome` VARCHAR(100) NOT NULL,
    `descricao` VARCHAR(250) NOT NULL,
    `quantidade` INT NOT NULL,
    `data_vencimento` DATETIME DEFAULT CURRENT_TIMESTAMP, 
    `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP,
    `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`))
ENGINE = InnoDB;

ALTER TABLE produtos MODIFY COLUMN data_vencimento DATETIME;


insert into produtos (nome, descricao, quantidade) values ('Paracetamol', 'comprimido para dor de cabeça', 10);