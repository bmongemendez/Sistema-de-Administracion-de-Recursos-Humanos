-- MySQL Script generated by MySQL Workbench
-- Sat Mar  6 15:38:49 2021
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema sahr.application
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema sahr.application
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sahr.application` DEFAULT CHARACTER SET utf8mb4 ;
-- -----------------------------------------------------
-- Schema sahr.identitycontext
-- -----------------------------------------------------
USE `sahr.application` ;

-- -----------------------------------------------------
-- Table `sahr.application`.`AspNetUsersRef`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`AspNetUsersRef` (
  `userName` VARCHAR(256) NOT NULL,
  `ligthVersionEnabled` TINYINT(1) NOT NULL DEFAULT 0,
  `rightToLeftEnabled` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`userName`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Empleados`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Empleados` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `cedula` INT NOT NULL,
  `nombre` VARCHAR(50) NOT NULL,
  `apellido1` VARCHAR(50) NOT NULL,
  `apellido2` VARCHAR(50) NULL,
  `fechaNacimiento` DATE NOT NULL,
  `telefono` VARCHAR(50) NOT NULL,
  `telefonoEmergencia` VARCHAR(50) NULL,
  `contactoEmergencia` VARCHAR(50) NULL,
  `tieneBachiller` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciatura` TINYINT(1) NULL DEFAULT 0,
  `tieneTecnico` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaA3` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaB1` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaB2` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaB3` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaD` TINYINT(1) NULL DEFAULT 0,
  `tieneLicenciaE` TINYINT(1) NULL DEFAULT 0,
  `seElimino` TINYINT(1) NOT NULL DEFAULT 0,
  `userName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `cedula_UNIQUE` (`cedula` ASC) VISIBLE,
  INDEX `idUser_Empleados_idx` (`userName` ASC) VISIBLE,
  CONSTRAINT `userNameEmpleados`
    FOREIGN KEY (`userName`)
    REFERENCES `sahr.application`.`AspNetUsersRef` (`userName`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Puestos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Puestos` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `siglas` VARCHAR(10) NOT NULL,
  `nombre` VARCHAR(128) NOT NULL,
  `salarioMes` DECIMAL(13,3) NULL,
  `salarioDia` DECIMAL(9,3) NULL,
  `salarioHora` DECIMAL(9,3) NULL,
  `salarioMesJm` DECIMAL(13,3) NULL,
  `salarioDiaJm` DECIMAL(9,3) NULL,
  `salarioHoraJm` DECIMAL(9,3) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`IngresoContrato`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`IngresoContrato` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `inicio` DATE NOT NULL,
  `idPuesto` INT NOT NULL,
  `salarioDefinidoDia` DECIMAL NOT NULL,
  `cargoEspecifico` VARCHAR(70) NULL,
  INDEX `idPuesto_idx` (`idPuesto` ASC) VISIBLE,
  PRIMARY KEY (`id`),
  INDEX `idEmpleadoIC_idx` (`idEmpleado` ASC) VISIBLE,
  CONSTRAINT `idEmpleadoIC`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idPuestoIC`
    FOREIGN KEY (`idPuesto`)
    REFERENCES `sahr.application`.`Puestos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Vacaciones`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Vacaciones` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `fechaSolicitud` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fueronAprobadas` TINYINT(1) NOT NULL DEFAULT 0,
  `notas` VARCHAR(128) NULL,
  `idTiempo` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `idEmpleadoVacaciones_idx` (`idEmpleado` ASC) VISIBLE,
  INDEX `idTiempoVacaciones_idx` (`idTiempo` ASC) VISIBLE,
  CONSTRAINT `idEmpleadoVacaciones`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idTiempoVacaciones`
    FOREIGN KEY (`idTiempo`)
    REFERENCES `sahr.application`.`Tiempo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`FinContrato`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`FinContrato` (
  `idInicioContrato` INT NOT NULL,
  `fechaFin` DATE NOT NULL,
  `preavisoEjercido` INT(1) NULL DEFAULT 0,
  `diasPendientesPreaviso` INT NULL DEFAULT 0,
  `motivoSalida` INT(1) NOT NULL DEFAULT 0 COMMENT '3: despido.respnbld.patronal\n2: despido.sin.respnbld.patronal\n0: renuncia\n1: renuncia.respnbld.patronal',
  `saldoVacaciones` INT NOT NULL,
  `aguinaldo` DECIMAL(13,3) NULL,
  `cesantia` DECIMAL(13,3) NULL,
  `vacaciones` DECIMAL(13,3) NULL,
  `preaviso` DECIMAL(13,3) NULL,
  PRIMARY KEY (`idInicioContrato`),
  INDEX `idInicioContratoFN_idx` (`idInicioContrato` ASC) VISIBLE,
  CONSTRAINT `idInicioContratoFN`
    FOREIGN KEY (`idInicioContrato`)
    REFERENCES `sahr.application`.`IngresoContrato` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Tiempo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Tiempo` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `fechaInicio` DATE NOT NULL,
  `fechaFin` DATE NOT NULL,
  `idContrato` INT NOT NULL,
  `esLaborado` TINYINT(1) NULL DEFAULT 0,
  `esInjustificado` TINYINT(1) NULL DEFAULT 0,
  `esVacaciones` TINYINT(1) NULL DEFAULT 0,
  `esIncapacidad` TINYINT(1) NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  INDEX `idEmpeladoT_idx` (`idEmpleado` ASC) VISIBLE,
  INDEX `idContratoT_idx` (`idContrato` ASC) VISIBLE,
  CONSTRAINT `idEmpeladoT`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idContratoT`
    FOREIGN KEY (`idContrato`)
    REFERENCES `sahr.application`.`IngresoContrato` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Pagos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Pagos` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `idContrato` INT NOT NULL,
  `idTiempo` INT NOT NULL,
  `horasNormal` DECIMAL(7,3) NOT NULL,
  `horasExtra` DECIMAL(7,3) NULL DEFAULT 0,
  `diaDescanso` DECIMAL(10,3) NULL DEFAULT 0,
  `salarioNormal` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `salarioExtras` DECIMAL(13,3) NULL DEFAULT 0,
  `salarioDiaDescanso` DECIMAL(13,3) NULL DEFAULT 0,
  `salarioBruto` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `deducciones` DECIMAL(13,3) NULL DEFAULT 0,
  `cuentasPorPagar` DECIMAL(13,3) NULL DEFAULT 0,
  `salarioNeto` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `patronoCCSS` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `patronoROtrasInstituciones` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `patronoLPT` DECIMAL(13,3) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(128) NULL,
  `seElimino` TINYINT(1) NOT NULL DEFAULT 0,
  `userName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `idEmpleadoP_idx` (`idEmpleado` ASC) VISIBLE,
  INDEX `idContratoP_idx` (`idContrato` ASC) VISIBLE,
  INDEX `idUserPagos_idx` (`userName` ASC) VISIBLE,
  INDEX `idTiempoPagos_idx` (`idTiempo` ASC) VISIBLE,
  CONSTRAINT `idEmpleadoPagos`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idContratoPagos`
    FOREIGN KEY (`idContrato`)
    REFERENCES `sahr.application`.`IngresoContrato` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `userNamePagos`
    FOREIGN KEY (`userName`)
    REFERENCES `sahr.application`.`AspNetUsersRef` (`userName`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idTiempoPagos`
    FOREIGN KEY (`idTiempo`)
    REFERENCES `sahr.application`.`Tiempo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Deducciones`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Deducciones` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `grupo` CHAR(3) NOT NULL,
  `concepto` VARCHAR(128) NOT NULL,
  `patrono` DECIMAL(5,3) NULL DEFAULT 0,
  `trabajador` DECIMAL(5,3) NULL DEFAULT 0,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Aguinaldos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Aguinaldos` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `idContrato` INT NOT NULL,
  `fechaInicio` DATE NOT NULL,
  `fechaFin` DATE NOT NULL,
  `sumatoriaSalarioBrutos` DECIMAL(10,3) NOT NULL,
  `montoAguinaldo` DECIMAL(10,3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `idContratoP_idx` (`idContrato` ASC) VISIBLE,
  INDEX `idEmpleadoA_idx` (`idEmpleado` ASC) VISIBLE,
  CONSTRAINT `idEmpleadoA`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idContratoA`
    FOREIGN KEY (`idContrato`)
    REFERENCES `sahr.application`.`IngresoContrato` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Evaluaciones`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Evaluaciones` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `calificacion` INT(2) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(128) NULL,
  PRIMARY KEY (`id`),
  INDEX `idEmpleado_Eval_idx` (`idEmpleado` ASC) VISIBLE,
  CONSTRAINT `idEmpleadoEval`
    FOREIGN KEY (`idEmpleado`)
    REFERENCES `sahr.application`.`Empleados` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`EmpleadosRegistroAuditoria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`EmpleadosRegistroAuditoria` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idEmpleado` INT NOT NULL,
  `columna` VARCHAR(255) NOT NULL,
  `valorAnterior` VARCHAR(255) NULL,
  `valorNuevo` VARCHAR(255) NULL,
  `fechaHoraTransaccion` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `userName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`PagosRegistroAuditoria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`PagosRegistroAuditoria` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idPago` INT NOT NULL,
  `columna` VARCHAR(255) NOT NULL,
  `valorAnterior` VARCHAR(255) NULL,
  `valorNuevo` VARCHAR(255) NULL,
  `fechaHoraTransaccion` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `userName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Incapacidades`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Incapacidades` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `tipo` INT NOT NULL,
  `idTiempo` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idTiempo_UNIQUE` (`idTiempo` ASC) VISIBLE,
  CONSTRAINT `idTiempoIncapacidades`
    FOREIGN KEY (`idTiempo`)
    REFERENCES `sahr.application`.`Tiempo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sahr.application`.`Evidencias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sahr.application`.`Evidencias` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idIncapacidad` INT NOT NULL,
  `evidencia` MEDIUMBLOB NOT NULL,
  `tipo` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `idIncapacidadEvidencias_idx` (`idIncapacidad` ASC) VISIBLE,
  CONSTRAINT `idIncapacidadEvidencias`
    FOREIGN KEY (`idIncapacidad`)
    REFERENCES `sahr.application`.`Incapacidades` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `sahr.application`;

DELIMITER $$
USE `sahr.application`$$
CREATE DEFINER = CURRENT_USER TRIGGER `sahr.application`.`Empleados_AFTER_INSERT` 
AFTER INSERT ON `Empleados` 
FOR EACH ROW
BEGIN
	INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'cedula', OLD.cedula, NEW.cedula, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'nombre', OLD.nombre, NEW.nombre, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'apellido1', OLD.apellido1, NEW.apellido1, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'apellido2', OLD.apellido2, NEW.apellido2, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'fechaNacimiento', OLD.fechaNacimiento, NEW.fechaNacimiento, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'telefono', OLD.telefono, NEW.telefono, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'telefonoEmergencia', OLD.telefonoEmergencia, NEW.telefonoEmergencia, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'contactoEmergencia', OLD.contactoEmergencia, NEW.contactoEmergencia, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneBachiller', OLD.tieneBachiller, NEW.tieneBachiller, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciatura', OLD.tieneLicenciatura, NEW.tieneLicenciatura, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneTecnico', OLD.tieneTecnico, NEW.tieneTecnico, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaA3', OLD.tieneLicenciaA3, NEW.tieneLicenciaA3, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaB1', OLD.tieneLicenciaB1, NEW.tieneLicenciaB1, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaB2', OLD.tieneLicenciaB2, NEW.tieneLicenciaB2, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaB3', OLD.tieneLicenciaB3, NEW.tieneLicenciaB3, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaD', OLD.tieneLicenciaD, NEW.tieneLicenciaD, NEW.userName);
  INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
  VALUES (NEW.id, 'tieneLicenciaE', OLD.tieneLicenciaE, NEW.tieneLicenciaE, NEW.userName);
END$$

USE `sahr.application`$$
CREATE DEFINER = CURRENT_USER TRIGGER `sahr.application`.`Empleados_AFTER_UPDATE` 
AFTER UPDATE ON `Empleados` 
FOR EACH ROW
BEGIN
	IF OLD.seElimino <> NEW.seElimino THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'seElimino', OLD.seElimino, NEW.seElimino, NEW.userName);
  END IF;
  IF OLD.cedula <> NEW.cedula THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'cedula', OLD.cedula, NEW.cedula, NEW.userName);
  END IF;
  IF OLD.nombre <> NEW.nombre THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'nombre', OLD.nombre, NEW.nombre, NEW.userName);
  END IF;
  IF OLD.apellido1 <> NEW.apellido1 THEN
      INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'apellido1', OLD.apellido1, NEW.apellido1, NEW.userName);
  END IF;
  IF IFNULL(OLD.apellido2, '') <> IFNULL(NEW.apellido2, '') THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'apellido2', OLD.apellido2, NEW.apellido2, NEW.userName);
  END IF;
  IF OLD.fechaNacimiento <> NEW.fechaNacimiento THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'fechaNacimiento', OLD.fechaNacimiento, NEW.fechaNacimiento, NEW.userName);
  END IF;
  IF OLD.telefono <> NEW.telefono THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'telefono', OLD.telefono, NEW.telefono, NEW.userName);
  END IF;
  IF IFNULL(OLD.telefonoEmergencia, '') <> IFNULL(NEW.telefonoEmergencia, '') THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'telefonoEmergencia', OLD.telefonoEmergencia, NEW.telefonoEmergencia, NEW.userName);
  END IF;
  IF IFNULL(OLD.contactoEmergencia, '') <> IFNULL(NEW.contactoEmergencia, '') THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'contactoEmergencia', OLD.contactoEmergencia, NEW.contactoEmergencia, NEW.userName);
  END IF;
  IF OLD.tieneBachiller <> NEW.tieneBachiller THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneBachiller', OLD.tieneBachiller, NEW.tieneBachiller, NEW.userName);
  END IF;
  IF OLD.tieneLicenciatura <> NEW.tieneLicenciatura THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciatura', OLD.tieneLicenciatura, NEW.tieneLicenciatura, NEW.userName);
  END IF;
  IF OLD.tieneTecnico <> NEW.tieneTecnico THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneTecnico', OLD.tieneTecnico, NEW.tieneTecnico, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaA3 <> NEW.tieneLicenciaA3 THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaA3', OLD.tieneLicenciaA3, NEW.tieneLicenciaA3, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaB1 <> NEW.tieneLicenciaB1 THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaB1', OLD.tieneLicenciaB1, NEW.tieneLicenciaB1, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaB2 <> NEW.tieneLicenciaB2 THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaB2', OLD.tieneLicenciaB2, NEW.tieneLicenciaB2, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaB3 <> NEW.tieneLicenciaB3 THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaB3', OLD.tieneLicenciaB3, NEW.tieneLicenciaB3, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaD <> NEW.tieneLicenciaD THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaD', OLD.tieneLicenciaD, NEW.tieneLicenciaD, NEW.userName);
  END IF;
  IF OLD.tieneLicenciaE <> NEW.tieneLicenciaE THEN
    INSERT INTO `EmpleadosRegistroAuditoria`(idEmpleado, columna, valorAnterior, valorNuevo, userName) 
		VALUES (NEW.id, 'tieneLicenciaE', OLD.tieneLicenciaE, NEW.tieneLicenciaE, NEW.userName);
  END IF;
END$$

USE `sahr.application`$$
CREATE DEFINER = CURRENT_USER TRIGGER `sahr.application`.`Pagos_AFTER_INSERT` AFTER INSERT ON `Pagos` FOR EACH ROW
BEGIN
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'idEmpleado', NEW.idEmpleado, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'idContrato', NEW.idContrato, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'idTiempo', NEW.idTiempo, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'horasNormal', NEW.horasNormal, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'horasExtra', NEW.horasExtra, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'diaDescanso', NEW.diaDescanso, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'salarioNormal', NEW.salarioNormal, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'salarioExtras', NEW.salarioExtras, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'salarioDiaDescanso', NEW.salarioDiaDescanso, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'salarioBruto', NEW.salarioBruto, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'deducciones', NEW.deducciones, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'cuentasPorPagar', NEW.cuentasPorPagar, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'salarioNeto', NEW.salarioNeto, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'patronoCCSS', NEW.patronoCCSS, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'patronoROtrasInstituciones', NEW.patronoROtrasInstituciones, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'patronoLPT', NEW.patronoLPT, NEW.userName);
  INSERT INTO `PagosRegistroAuditoria`(idPago, columna, valorNuevo, userName)
  VALUES (NEW.id, 'observaciones', NEW.observaciones, NEW.userName);
END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

use `sahr.application`;

insert into aspnetusersref values ('rrhh');
commit;

use `sahr.application`;
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (179781656, 'Gabi', 'Klemke', '2021-01-07', '7946944878', 'rrhh');
commit;

insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (390362943, 'Ozzy', 'Selwyn', '2021-01-10', '2613827643', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (254710394, 'Dusty', 'Dils', '2020-08-21', '6757651774', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (338420473, 'Merrili', 'Dampier', '2020-11-23', '6363109595', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (189696583, 'Marney', 'Laws', '2020-12-21', '8126633297', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (387834523, 'Leslie', 'Issakov', '2020-06-20', '2823547808', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (220319553, 'Annalise', 'Hodgins', '2020-05-08', '2616052724', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (195304313, 'Gertrude', 'Waiting', '2020-07-11', '3103306007', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (185887100, 'Joya', 'Sans', '2021-01-22', '9578333177', 'rrhh');
insert into empleados (cedula, nombre, apellido1, fechaNacimiento, telefono, userName) values (227702360, 'Mei', 'Hanbury-Brown', '2020-04-21', '7486047919', 'rrhh');
commit;

insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOCG', 'Agente de ventas*', 358468.86, 12802.46, 1600.31, 1760.34, 14082.71, 394315.75);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Albañil', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSC', 'Alistador automotriz', 323376.20, 11549.15, 1443.64, 1588.01, 12704.07, 50816.26);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSC', 'Ayudante de mecánico general', 323376.20, 11549.15, 1443.64, 1588.01, 12704.07, 50816.26);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSC', 'Ayudante operario, construcción', 323376.20, 11549.15, 1443.64 , 1588.01, 12704.07, 50816.26);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('Bach', 'Bachiller universitario', 567118.50, 20254.23, 2531.78, 2784.96, 22279.66, 89118.62);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSCG', 'Bodeguero  (encargado)', 341004.39, 12178.73, 1522.34, 1674.58, 13396.60, 53586.40);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TONCG', 'Bodeguero (peón) *', 316964.69, 11320.17, 1415.02, 1556.52, 12452.18, 49808.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOE', 'Chofer de trailer', 388435.60, 13872.70, 1734.09, 1907.50, 15259.97, 61039.88);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSC', 'Chofer de vehículo liviano', 315396.76, 11264.17, 1408.02, 1548.82, 12390.59, 49562.35);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Chofer de vehículo pesado', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TMED', 'Contador privado*', 375649.82, 13416.07, 1677.01, 1844.71, 14757.67, 59030.69);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('DES', 'Contador privado*', 500000.15, 17857.15, 2232.14, 2455.36, 19642.86, 78571.45);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('BACH', 'Contador privado*', 567118.50, 20254.23, 2531.78, 2784.96, 22279.66, 89118.62);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('LIC', 'Contador privado*', 680565.53, 24305.91, 3038.24, 3342.06, 26736.50, 106946.01);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOCG', 'Dibujante ingeniería, arquitectura*', 358468.86, 12802.46, 1600.31, 1760.34, 14082.71, 56330.82);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('DES', 'Diplomado parauniversitario*', 500000.15, 17857.15, 2232.14, 2455.36, 19642.86, 78571.45);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('DES', 'Diplomado universitario*', 500000.15, 17857.15, 2232.14, 2455.36, 19642.86, 78571.45);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Electricista', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Enderezador automotriz', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TMED', 'Graduado del INA*', 375649.82, 13416.07, 1677.01, 1844.71, 14757.67, 59030.69);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSCG', 'Guarda*', 341004.39, 12178.73, 1522.34, 1674.58, 13396.60, 53586.40);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Laboratorista civil', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('LIC', 'Licenciado universitario*', 680565.53, 24305.91, 3038.24, 3342.06, 26736.50, 106946.01);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOE', 'Maestro de obras (construcción)', 378850.64, 13530.38, 1691.30, 1860.43, 14883.42, 59533.67);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Mecánico general', 311968.44, 11141.73, 1392.72, 1531.99, 12255.90, 49023.61);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TONCG', 'Misceláneo*', 316964.70, 11320.17, 1415.02, 1556.52, 12452.18, 49808.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOSCG', 'Oficinista (general)*' , 341004.39, 12178.73, 1522.34, 1674.58, 13396.60, 53586.40);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOE', 'Operador de draga', 388435.60, 13872.70, 1734.09, 1907.50, 15259.97, 61039.88);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Operador de maquinaria pesada', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Operario en construcción', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TONC', 'Peón en general', 297377.36, 10620.62, 1327.58, 1460.34, 11682.68, 46730.73);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TONC', 'Peón de construcción' , 297377.36, 10620.62, 1327.58, 1460.34, 11682.68, 46730.73);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOE', 'Pintor automotriz', 388435.60, 13872.70, 1734.09, 1907.50, 15259.97, 61039.88);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOCG', 'Proveedor*', 358468.86, 12802.46, 1600.31, 1760.34, 14082.71, 56330.82);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Tractorista (oruga o llanta)', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
insert into puestos (siglas, nombre, salarioMes, salarioDia, salarioHora, salarioHoraJm, salarioDiaJm, salarioMesJm) values ('TOC', 'Vagonetero', 329329.28, 11761.76, 1470.22, 1617.24, 12937.94, 51751.74);
commit;

insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`, `trabajador`) values ('A', 'SEM', '9.25', '5.50');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`, `trabajador`) values ('A', 'IVM', '5.25', '4');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('B', 'Cuota Banco Popular', '0.25');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('B', 'Asignaciones Familiares', '5');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('B', 'IMAS', '0.50');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('B', 'INA', '1.50');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('C', 'Aporte Patrono Banco Popular', '0.25');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('C', 'Fondo Capitalizacion Laboral', '3');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('C', 'Fondo de Pensiones Complementarias', '0.50');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `trabajador`) values ('C', 'Aporte Trabajador Banco Popular', '1');
insert into `sahr.application`.`deducciones` (`grupo`, `concepto`, `patrono`) values ('C', 'INS', '1');
commit;

-- insert into `fincontrato` (`idInicioContrato`,`fechaFin`,`preavisoEjercido`,`diasPendientesPreaviso`,`motivoSalida`,`saldoVacaciones`,`aguinaldo`,`cesantia`,`vacaciones`,`preaviso`) VALUES (7,'2022-12-01',1,0,0,10,30000.000,30000.000,100.000,0.000);
-- commit;