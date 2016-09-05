<?php
$idAdmin=$_POST['admin'];
$nombre=$_POST['nombre'];

$numero=$_POST['numero'];
//tiempos
$tiempoVuelta=$_POST['tiempoVuelta'];
$tiempoFaena=$_POST['tiempoFaena'];
$tiempoEmin=$_POST['tiempoEmin'];
$tiempoEmax=$_POST['tiempoEmax'];
//datos
$tonelaje=$_POST['tonelaje'];
$cargarMin=$_POST['cargarMin'];
$cargarMax=$_POST['cargarMax'];
$reps=$_POST['reps'];
$caidaPer=$_POST['caidaPer'];


//choques
$zipper=$_POST['zipper'];
$intpd=$_POST['intpd'];
$intpi=$_POST['intpi'];
$intmd=$_POST['intmd'];
$intp=$_POST['intp'];
$intb=$_POST['intb'];
$cabina=$_POST['cabina'];

$area=$_POST['area'];
$descArea=$_POST['descArea'];
$mercedes=$_POST['mercedes'];
$descCamion=$_POST['descCamion'];

$check1=$_POST['check1'];
$check2=$_POST['check2'];

$descChoque=$_POST['descChoque'];


//pruebas
$orden=$_POST['orden'];
$cantpreguntas=$_POST['cantpreguntas'];
$preguntas=$_POST['preguntas'];

include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());

$query0=mysql_query("Select Nombre from Administrador_Nivel where Nombre=\"".$nombre."\"") or die("-2");
$result = mysql_fetch_array($query0);
$respuesta=$result[0];

if($respuesta!=""){ 
	echo "ya creado"; 
	return; 
}
else{
	$query = mysql_query("INSERT INTO Nivel (NumeroNivel, TiempoVuelta, TiempoFaena, tiempoExtMin, tiempoExtMax, CantidadVueltas, ChoqueZipper, AreaExtraccion, CamionMercedes, IntPosterior, IntPosteriorDer, IntPosteriorIzq, IntMedioDer, IntCabina, IntBrazo, descuentoChoque, ExitoPreguntas, CantPreguntas, descuentoTunel, descuentoCamion, checklist1, checklist2, minimoCargar, maximoCargar, TonelajeALlevar, CaidaPermitida) values (\"". $numero . "\",\"".$tiempoVuelta."\",\"".$tiempoFaena."\",\"".$tiempoEmin."\",\"".$tiempoEmax."\",\"".$reps."\",\"".$zipper."\",\"".$area."\",\"".$mercedes."\",\"".$intp."\",\"".$intpd."\",\"".$intpi."\",\"".$intmd."\",\"".$cabina."\",\"".$intb."\",\"". $descChoque."\",\"". $preguntas."\",\"". $cantpreguntas."\",\"".$descArea."\",\"".$descCamion."\",\"".$check1."\",\"".$check2."\",\"".$cargarMin."\",\"".$cargarMax."\",\"".$tonelaje."\",\"".$caidaPer."\")") or die("-3");
	$idNivel=mysql_insert_id();
	
	$query2=mysql_query("insert into Administrador_Nivel(IdAdministrador,Nombre,IdNivel) values(\"" . $idAdmin . "\",\"".$nombre."\",\"".$idNivel."\")") or die("-4");
	
	echo "correcto";
}
?>