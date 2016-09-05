<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$validar=mysql_query("Select IdAdministrador from Administrador Where IdAdministrador=\"".$idAdmin."\"") or die("-1");
//aqui ver si existe y devolver

$query0=mysql_query("UPDATE `Nivel` SET `TiempoVuelta`=\"".$_POST["tiempoVuelta"]."\",`TiempoFaena`=\"".$_POST["tiempoFaena"]."\",`tiempoExtMin`=\"".$_POST["tiempoExtMin"]."\",`tiempoExtMax`=\"".$_POST["tiempoExtMax"]."\",`CantidadVueltas`=\"".$_POST["reps"]."\",`ChoqueZipper`=\"".$_POST["zipper"]."\",`AreaExtraccion`=\"".$_POST["area"]."\",`CamionMercedes`=\"".$_POST["mercedes"]."\",`IntPosterior`=\"".$_POST["intp"]."\",`IntPosteriorDer`=\"".$_POST["intpd"]."\",`IntPosteriorIzq`=\"".$_POST["intpi"]."\",`IntMedioDer`=\"".$_POST["intmd"]."\",`IntCabina`=\"".$_POST["cabina"]."\",`IntBrazo`=\"".$_POST["intb"]."\",`ExitoPreguntas`=\"".$_POST["preguntas"]."\",`CantPreguntas`=\"".$_POST["cantpreguntas"]."\",`minimoCargar`=\"".$_POST["cargarMin"]."\",`maximoCargar`=\"".$_POST["cargarMax"]."\",`TonelajeALlevar`=\"".$_POST["tonelaje"]."\",`CaidaPermitida`=\"".$_POST["caidaPer"]."\",`descuentoChoque`=\"".$_POST["descChoque"]."\",`checklist1`=\"".$_POST["check1"]."\",`checklist2`=\"".$_POST["check2"]."\",`descuentoTunel`=\"".$_POST["descArea"]."\",`descuentoCamion`=\"".$_POST["descCamion"]."\" WHERE IdNivel=\"".$_POST["idniv"]."\"") or die("-2");



echo "correcto";
?>