<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
//set_include_path(get_include_path() . PATH_SEPARATOR . 'Classes/');
require_once 'Classes/PHPExcel.php';
include "Classes/PHPExcel/IOFactory.php";
require_once 'Classes/PHPExcel/Cell/AdvancedValueBinder.php';
require_once("PHPMailer_5.2.4/class.phpmailer.php");
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());

mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$query = mysql_query("select Mail from Administrador where IdAdministrador  = \"" . $_POST["id"] . "\"")
or die("Error en la query" . mysql_error());
$resultQuery = mysql_fetch_array($query);
$correo = $resultQuery['Mail']; //"ricardoconchasaldivia@gmail.com"; //

$objPHPExcel = new PHPExcel();
$objReader = PHPExcel_IOFactory::createReader('Excel2007');
$objPHPExcel = $objReader->load("Historial".$_POST["numeroModulo"].".xlsx");

$objPHPExcel->setActiveSheetIndex(0);
PHPExcel_Cell::setValueBinder( new PHPExcel_Cell_AdvancedValueBinder() );
//Información General
$objPHPExcel->getActiveSheet()->SetCellValue('D2',$_POST["nombreAlumno"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D3',$_POST["numeroModulo"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D4',$_POST["fecha"]);
//tiempo
$minutos = round($_POST["tiempoO"] / 60, 0, PHP_ROUND_HALF_DOWN);
$segundos = $_POST["tiempoO"] % 60;
$tiempo = (($minutos < 10) ? ("0" . $minutos) : "" . $minutos) . ":" . (($segundos < 10) ? ("0" . $segundos) : "" . $segundos);
$objPHPExcel->getActiveSheet()->SetCellValue('D7',$_POST["tiempoE"].":00" );
$objPHPExcel->getActiveSheet()->SetCellValue('D8', $tiempo );
//preguntas
if($_POST["numeroModulo"] == "1" || $_POST["numeroModulo"] == "2" || $_POST["numeroModulo"] == "3" || $_POST["numeroModulo"] == "12" || $_POST["numeroModulo"] == "15" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('D11', $_POST["aprobacion"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D12', $_POST["aprobacionO"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D13', $_POST["cantpreguntas"]);
}
//Integridad Maquina
if($_POST["numeroModulo"] == "6" || $_POST["numeroModulo"] == "7" || $_POST["numeroModulo"] == "8" || $_POST["numeroModulo"] == "9" || $_POST["numeroModulo"] == "10" || $_POST["numeroModulo"] == "11" || $_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('D16',$_POST["intexigido"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D17',$_POST["intO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D18',$_POST["postE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D19',$_POST["postO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D20',$_POST["postIE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D21', $_POST["postIO"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D22',$_POST["postDE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D23', $_POST["postDO"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D24',$_POST["medioDE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D25',$_POST["medioDO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D26', $_POST["cabE"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D27', $_POST["cabO"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D28',$_POST["baldeE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D29',$_POST["baldeO"] );
}
//Integridad Tunel
if($_POST["numeroModulo"] == "5" || $_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('D32',$_POST["tunelE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D33', $_POST["desctunel"]);
$objPHPExcel->getActiveSheet()->SetCellValue('D34',$_POST["tunelO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D35', $_POST["canttunel"]);
}
//Check1
if($_POST["numeroModulo"] == "4" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('D38',$_POST["checkE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D39',$_POST["checkO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D40',$_POST["revFunc1"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D41',$_POST["revEst1"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D42',$_POST["revCab1"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D43', $_POST["prevRies1"]);
}
//Si no's
if($_POST["numeroModulo"] == "6" || $_POST["numeroModulo"] == "7" || $_POST["numeroModulo"] == "8" || $_POST["numeroModulo"] == "9" || $_POST["numeroModulo"] == "10" || $_POST["numeroModulo"] == "11" || $_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$val = "Si";
if($_POST["terminoFaena"] == 0) $val = "No";
$objPHPExcel->getActiveSheet()->SetCellValue('D45', $val);
$objPHPExcel->getActiveSheet()->SetCellValue('D46',$_POST["ENS"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D47',$_POST["ENO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D48',$_POST["correctoTraslado"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D49',$_POST["avanceMotor"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D50',$_POST["avanceBalde"] );
$objPHPExcel->getActiveSheet()->SetCellValue('D51',$_POST["ordenEj"] );
}
//vueltas
if($_POST["numeroModulo"] == "6" || $_POST["numeroModulo"] == "7" || $_POST["numeroModulo"] == "8" || $_POST["numeroModulo"] == "9" || $_POST["numeroModulo"] == "10" || $_POST["numeroModulo"] == "11" || $_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('F7',$_POST["vueltasE"]);
$objPHPExcel->getActiveSheet()->SetCellValue('F8',$_POST["vueltasR"] );
//$objPHPExcel->getActiveSheet()->SetCellValue('F9',$_POST["vueltasC"] );
}
//metas Faena
if($_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('F16',$_POST["tonelajeE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F17',$_POST["tonelajeO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F18',$_POST["caidaPer"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F19', $_POST["caidaO"]);
$objPHPExcel->getActiveSheet()->SetCellValue('F20',$_POST["correctoCarguio"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F21',$_POST["patinaje"] );
}
//int camion
if($_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('F32',$_POST["camionE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F33',$_POST["desccamion"]);
$objPHPExcel->getActiveSheet()->SetCellValue('F34',$_POST["camionO"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F35',$_POST["cantcamion"] );
}
//Check2
if($_POST["numeroModulo"] == "13" || $_POST["numeroModulo"] == "14-a" || $_POST["numeroModulo"] == "14-b" || $_POST["numeroModulo"] == "16" || $_POST["numeroModulo"] == "17" || $_POST["numeroModulo"] == "18"){
$objPHPExcel->getActiveSheet()->SetCellValue('F38', $_POST["checkE2"]);
$objPHPExcel->getActiveSheet()->SetCellValue('F39', $_POST["checkO2"]);
$objPHPExcel->getActiveSheet()->SetCellValue('F40',$_POST["revFunc2"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F41',$_POST["revEst2"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F42',$_POST["revCab2"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F43', $_POST["prevRies2"]);
}
//ziper
if($_POST["numeroModulo"] == "6" || $_POST["numeroModulo"] == "7" || $_POST["numeroModulo"] == "8" || $_POST["numeroModulo"] == "9" || $_POST["numeroModulo"] == "10" || $_POST["numeroModulo"] == "11"){
$objPHPExcel->getActiveSheet()->SetCellValue('F46',$_POST["zipperE"] );
$objPHPExcel->getActiveSheet()->SetCellValue('F47',$_POST["zipperO"] );
//$objPHPExcel->getActiveSheet()->SetCellValue('F48',$_POST["cantzipper"] );
}

$objPHPExcel->setActiveSheetIndex(0);

$nombre2=$_POST["nombreArchivo"];
$nombre='Historial -'.$nombre2.'.xlsx';

header('Content-Type: application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
header('Content-Disposition: attachment;filename=""');
header('Cache-Control: max-age=0');
$objWriter = PHPExcel_IOFactory::createWriter($objPHPExcel, 'Excel2007');
$objWriter->save($nombre);

//echo $correo;
//$objWriter->save('php://output');
//echo "hola";
$mail = new PHPMailer();
$mail->CharSet ='iso-8859-1';
$mail->IsSMTP();                                      // set mailer to use SMTP
$mail->Host = "aslan.fullxhosting.com";  // specify main and backup server
$mail->SMTPAuth = true;     // turn on SMTP authentication
$mail->Username = "lhd@nemorisgames.com";  // SMTP username
$mail->Password = "1212121212lhd*"; // SMTP password$
$mail->SMTPSecure = "ssl";
$mail ->Port ='465';
$mail->From = "lhd@nemorisgames.com";
$mail->FromName = "Simulador";
$mail->AddAddress($correo);
$mail->AddAttachment($nombre); 

$mail->IsHTML(true);                                  // set email format to HTML

$mail->Subject = "Archivos Solicitados";
$mail->Body    = "Archivos Solicitados";
$mail->AltBody = "Archivos Solicitados";

if(!$mail->Send()){
   echo "Message could not be sent. <p>";
   echo "Mailer Error: " . $mail->ErrorInfo;
   exit;
}

echo "Message has been sent";
unlink($nombre);


exit;

?>