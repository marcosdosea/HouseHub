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
  `email` VARCHAR(50) NOT NULL,
  `bairro` VARCHAR(50) NULL,
  `estado` VARCHAR(45) NULL,
  `cidade` VARCHAR(45) NULL,
  `logradouro` VARCHAR(100) NULL,
  `cep` VARCHAR(8) NULL,
  `numero` VARCHAR(20) NULL,
  `complemento` VARCHAR(100) NULL,
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
  `precoAluguel` DECIMAL UNSIGNED NULL,
  `precoVenda` DECIMAL UNSIGNED NOT NULL,
  `iptu` DECIMAL UNSIGNED NOT NULL,
  `status` ENUM('Disponivel', 'Vendido', 'Alugado', 'Deletado') NOT NULL DEFAULT 'Disponivel',
  `precoCondominio` DECIMAL UNSIGNED NULL,
  `podeAnimal` TINYINT NOT NULL DEFAULT 0 COMMENT '0 = Não, 1 = Sim',
  `tipo` ENUM('Casa', 'Apartamento') NOT NULL DEFAULT 'Casa',
  `idPessoa` INT UNSIGNED NOT NULL,
  `bairro` VARCHAR(100) NOT NULL,
  `estado` VARCHAR(100) NOT NULL,
  `cidade` VARCHAR(100) NOT NULL,
  `cep` VARCHAR(8) NOT NULL,
  `logradouro` VARCHAR(45) NOT NULL,
  `numero` VARCHAR(45) NOT NULL,
  `complemento` VARCHAR(100) NULL,
  `modalidade` ENUM('Aluguel', 'Venda', 'Ambos') NOT NULL DEFAULT 'Ambos',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Imovel_Pessoa1_idx` (`idPessoa` ASC) VISIBLE,
  INDEX `descricao_idx` (`descricao` ASC) VISIBLE,
  INDEX `bairro_idx` (`bairro` ASC) INVISIBLE,
  INDEX `estado_idx` (`estado` ASC) INVISIBLE,
  INDEX `cidade_idx` (`cidade` ASC) INVISIBLE,
  INDEX `logradouro_idx` (`logradouro` ASC) VISIBLE,
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
  `status` ENUM('Pendente', 'Recusado', 'Agendado', 'Concluido') NOT NULL DEFAULT 'Pendente',
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
  `formaPagamento` ENUM('Dinheiro', 'Cartao de Credito', 'Transferencia Bancaria', 'Boleto', 'Pix') NOT NULL,
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


-- -----------------------------------------------------
-- Table `househub`.`Valores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Valores` (
  `id` INT UNSIGNED NOT NULL,
  `descricao` VARCHAR(45) NULL,
  `valor` DECIMAL NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `Descricao_UNIQUE` (`descricao` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`ResultadoAvaliacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`ResultadoAvaliacao` (
  `id` INT UNSIGNED NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `aprovado` TINYINT NULL DEFAULT 0 COMMENT '0 - Não\n1 - Sim',
  `dataFinalizado` DATE NULL,
  `Avaliacao_id` INT UNSIGNED NOT NULL,
  `Imovel_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_ResultadoAvaliacao_Avaliacao1_idx` (`Avaliacao_id` ASC) VISIBLE,
  INDEX `fk_ResultadoAvaliacao_Imovel1_idx` (`Imovel_id` ASC) VISIBLE,
  CONSTRAINT `fk_ResultadoAvaliacao_Avaliacao1`
    FOREIGN KEY (`Avaliacao_id`)
    REFERENCES `househub`.`Avaliacao` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ResultadoAvaliacao_Imovel1`
    FOREIGN KEY (`Imovel_id`)
    REFERENCES `househub`.`Imovel` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`Imagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`Imagem` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `URL` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `URL_UNIQUE` (`URL` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`ImovelImagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`ImovelImagem` (
  `Imovel_id` INT UNSIGNED NOT NULL,
  `Imagem_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`Imovel_id`, `Imagem_id`),
  INDEX `fk_Imovel_has_Imagem_Imagem1_idx` (`Imagem_id` ASC) VISIBLE,
  INDEX `fk_Imovel_has_Imagem_Imovel1_idx` (`Imovel_id` ASC) VISIBLE,
  CONSTRAINT `fk_Imovel_has_Imagem_Imovel1`
    FOREIGN KEY (`Imovel_id`)
    REFERENCES `househub`.`Imovel` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Imovel_has_Imagem_Imagem1`
    FOREIGN KEY (`Imagem_id`)
    REFERENCES `househub`.`Imagem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `househub`.`SolicitacaoReparoImagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `househub`.`SolicitacaoReparoImagem` (
  `SolicitacaoReparo_id` INT UNSIGNED NOT NULL,
  `Imagem_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`SolicitacaoReparo_id`, `Imagem_id`),
  INDEX `fk_SolicitacaoReparo_has_Imagem_Imagem1_idx` (`Imagem_id` ASC) VISIBLE,
  INDEX `fk_SolicitacaoReparo_has_Imagem_SolicitacaoReparo1_idx` (`SolicitacaoReparo_id` ASC) VISIBLE,
  CONSTRAINT `fk_SolicitacaoReparo_has_Imagem_SolicitacaoReparo1`
    FOREIGN KEY (`SolicitacaoReparo_id`)
    REFERENCES `househub`.`SolicitacaoReparo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SolicitacaoReparo_has_Imagem_Imagem1`
    FOREIGN KEY (`Imagem_id`)
    REFERENCES `househub`.`Imagem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;