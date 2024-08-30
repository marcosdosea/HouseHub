-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema househub
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema househub
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `househub` DEFAULT CHARACTER SET utf8 ;
USE `househub` ;

-- -----------------------------------------------------
-- Table `househub`.`Pessoa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Pessoa` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `cpf` VARCHAR(11) NOT NULL,
  `dataNascimento` DATE NOT NULL,
  `telefone` VARCHAR(15) NOT NULL,
  `email` VARCHAR(50) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) INVISIBLE,
  UNIQUE INDEX `cpf_UNIQUE` (`cpf` ASC) INVISIBLE,
  UNIQUE INDEX `telefone_UNIQUE` (`telefone` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Imovel`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Imovel` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(40) NOT NULL,
  `quartos` TINYINT UNSIGNED NOT NULL,
  `banheiros` TINYINT UNSIGNED NOT NULL,
  `endereco` VARCHAR(100) NOT NULL,
  `valorCondominio` DECIMAL UNSIGNED NOT NULL,
  `precoAluguel` DECIMAL UNSIGNED NOT NULL,
  `precoVenda` DECIMAL UNSIGNED NOT NULL,
  `iptu` DECIMAL UNSIGNED NOT NULL,
  `status` ENUM('Disponível', 'Vendido', 'Alugado') NOT NULL DEFAULT 'Disponível',
  `precoCondominio` DECIMAL UNSIGNED NOT NULL,
  `podeAnimal` TINYINT NOT NULL DEFAULT 0 COMMENT '0 = Não, 1 = Sim',
  `tipo` ENUM('Casa', 'Apartamento') NOT NULL DEFAULT 'Casa',
  `idPessoa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Imovel_Pessoa1_idx` (`idPessoa` ASC) VISIBLE,
  INDEX `endereco_idx` (`endereco` ASC) VISIBLE,
  INDEX `descricao_idx` (`descricao` ASC) VISIBLE,
  CONSTRAINT `fk_Imovel_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `househub`.`Pessoa` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Agendamento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Agendamento` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `observacoes` VARCHAR(50) NULL,
  `dataCriacao` DATE NOT NULL,
  `dataVisita` DATE NOT NULL,
  `status` ENUM('Pendente', 'Recusado', 'Agendado', 'Concluído') NOT NULL DEFAULT 'Pendente',
  `idImovel` INT UNSIGNED NOT NULL,
  `idPessoa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Agendamento_Imovel1_idx` (`idImovel` ASC) VISIBLE,
  INDEX `fk_Agendamento_Pessoa1_idx` (`idPessoa` ASC) VISIBLE,
  CONSTRAINT `fk_Agendamento_Imovel1`
    FOREIGN KEY (`idImovel`)
    REFERENCES `househub`.`Imovel` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_Agendamento_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `househub`.`Pessoa` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Locacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Locacao` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `dataContrato` DATE NOT NULL,
  `dataVencimento` DATE NOT NULL,
  `dataInicio` DATE NOT NULL,
  `dataFim` DATE NULL,
  `valor` DECIMAL UNSIGNED NOT NULL,
  `status` ENUM('Ativo', 'Inativo') NOT NULL DEFAULT 'Ativo',
  `idImovel` INT UNSIGNED NOT NULL,
  `idPessoa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Locacao_Imovel1_idx` (`idImovel` ASC) VISIBLE,
  INDEX `fk_Locacao_Pessoa1_idx` (`idPessoa` ASC) VISIBLE,
  CONSTRAINT `fk_Locacao_Imovel1`
    FOREIGN KEY (`idImovel`)
    REFERENCES `househub`.`Imovel` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_Locacao_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `househub`.`Pessoa` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Pagamento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Pagamento` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `dataPagamento` DATE NOT NULL,
  `formaPagamento` ENUM('Dinheiro', 'Cartão de Crédito', 'Transferência Bancária', 'Boleto', 'Pix') NULL,
  `pagamentoManual` TINYINT NOT NULL DEFAULT 0 COMMENT '0 = Não, 1 = Sim',
  `idLocacao` INT UNSIGNED NOT NULL,
  `status` ENUM('Pendente', 'Pago', 'Em atraso') NOT NULL DEFAULT 'Pendente',
  `dataVencimento` DATE NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Pagamento_Locacao1_idx` (`idLocacao` ASC) VISIBLE,
  CONSTRAINT `fk_Pagamento_Locacao1`
    FOREIGN KEY (`idLocacao`)
    REFERENCES `househub`.`Locacao` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Avaliacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Avaliacao` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `valorAprovado` DECIMAL UNSIGNED NOT NULL,
  `documento` VARCHAR(40) NOT NULL,
  `rendaMensal` DECIMAL UNSIGNED NOT NULL,
  `numeroDependentes` SMALLINT UNSIGNED NOT NULL,
  `scoreSerasa` SMALLINT UNSIGNED NOT NULL,
  `status` ENUM('Solicitado', 'Análise', 'Aprovado', 'Reprovado') NOT NULL DEFAULT 'Solicitado',
  `idPessoa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Avaliacao_Pessoa1_idx` (`idPessoa` ASC) INVISIBLE,
  CONSTRAINT `fk_Avaliacao_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `househub`.`Pessoa` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`SolicitacaoReparo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`SolicitacaoReparo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(100) NOT NULL,
  `valor` DECIMAL UNSIGNED NOT NULL,
  `enviarAlguem` TINYINT NOT NULL DEFAULT 1 COMMENT '0 = Não, 1 = Sim',
  `respostaProprietario` VARCHAR(100) NOT NULL,
  `data` DATE NOT NULL,
  `status` ENUM('Aberto', 'Avaliação', 'Concluído') NOT NULL DEFAULT 'Aberto',
  `idLocacao` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_SolicitacaoReparo_Locacao1_idx` (`idLocacao` ASC) INVISIBLE,
  INDEX `descricao_idx` (`descricao` ASC) INVISIBLE,
  CONSTRAINT `fk_SolicitacaoReparo_Locacao1`
    FOREIGN KEY (`idLocacao`)
    REFERENCES `househub`.`Locacao` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
