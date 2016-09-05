<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$query=mysql_query("INSERT INTO `Historial`(`IdNivel`, `Fecha`, `PorPreguntas`, `TiempoEmpleado`, `Check1`, `revFunc1`, `revEst1`, `revCab1`, `prevRies1`, `Check2`, `revFunc2`, `revEst2`, `revCab2`, `prevRies2`, `OrdenEj`, `MotorPunta`, `BaldePunta`, `VueltasCorrectas`, `EntregaNombrada`, `EntregaNombradaSup`, `TonelajeTotal`, `CaidaMat`, `CorrectoCarguio`, `Patinaje`, `IntMaquina`, `IntBalde`, `IntCabina`, `IntMedioDer`, `IntPost`, `IntPostIzq`, `IntPostDer`, `Zipper`, `CantZipper`, `Tunel`, `CantTunel`, `Camion`, `CantCamion`, `Traslado`, `CantVueltas`, `TerminoFaena`) VALUES (\"".$_POST['IdNivel']."\",\"".$_POST['Fecha']."\",\"".$_POST['PorPreguntas']."\",\"".$_POST['TiempoEmpleado']."\",\"".$_POST['Check1']."\",\"".$_POST['revFunc1']."\",\"".$_POST['revEst1']."\",\"".$_POST['revCab1']."\",\"".$_POST['prevRies1']."\",\"".$_POST['Check2']."\",\"".$_POST['revFunc2']."\",\"".$_POST['revEst2']."\",\"".$_POST['revCab2']."\",\"".$_POST['prevRies2']."\",\"".$_POST['OrdenEj']."\",\"".$_POST['MotorPunta']."\",\"".$_POST['BaldePunta']."\",\"".$_POST['VueltasCorrectas']."\",\"".$_POST['EntregaNombrada']."\",\"".$_POST['EntregaNombradaSup']."\",\"".$_POST['TonelajeTotal']."\",\"".$_POST['CaidaMat']."\",\"".$_POST['CorrectoCarguio']."\",\"".$_POST['Patinaje']."\",\"".$_POST['IntMaquina']."\",\"".$_POST['IntBalde']."\",\"".$_POST['IntCabina']."\",\"".$_POST['IntMedioDer']."\",\"".$_POST['IntPost']."\",\"".$_POST['IntPostIzq']."\",\"".$_POST['IntPostDer']."\",\"".$_POST['Zipper']."\",\"".$_POST['CantZipper']."\",\"".$_POST['Tunel']."\",\"".$_POST['CantTunel']."\",\"".$_POST['Camion']."\",\"".$_POST['CantCamion']."\",\"".$_POST['Traslado']."\",\"".$_POST['CantVueltas']."\",\"".$_POST['TerminoFaena']."\")") or die("-2");


$idHistorial=mysql_insert_id();
//$idAlumno=$_POST['idAlumno'];
// insertar en alumno historial 
$query2=mysql_query("insert into Historial_Alumno(IdAlumno,IdHistorial, Fecha) values(\"".$_POST['idAlumno']."\",".$idHistorial.",\"".$_POST['Fecha']."\")")or die("-3");
echo "correcto";
?>