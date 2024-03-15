<?php



header("Expires: Tue, 01 Jan 1980 1:00:00 GMT");



header("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT");



header("Cache-Control: no-store, no-cache, must-revalidate");



header("Cache-Control: post-check=0, pre-check=0", false);



header("Pragma: no-cache");



if (!defined('WEB_ROOT')) {



	exit;



}



if (isset($_GET['function'])){



      doLogout();



	  



}







$self = WEB_ROOT . 'admin/main.php';



?>



<html>



<head>



    



<title>Admin Panel</title>



<meta http-equiv="cache-control" content="max-age=0" />



<meta http-equiv="cache-control" content="no-cache" />



<meta http-equiv="expires" content="0" />



<meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />



<meta http-equiv="pragma" content="no-cache" />



<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />



<link rel="stylesheet" href="<?php echo WEB_ROOT;?>admin/include/jquery-ui.css">



 <link href="<?php echo WEB_ROOT;?>admin/include/admin.css" rel="stylesheet" type="text/css">



<script language="javascript" src="<?php echo WEB_ROOT;?>admin/library/jquery.min.js"></script>



<script language="javascript" src="<?php echo WEB_ROOT;?>admin/library/jquery.md5.min.js"></script> 



<script src="<?php echo WEB_ROOT;?>admin/include/jquery-ui.js"></script>



<script language="JavaScript" type="text/javascript" src="<?php echo WEB_ROOT;?>library/common.js"></script>



<link rel="shortcut icon" href="stylesheet/img/devil-icon.png" /> <!--Pemanggilan gambar favicon-->



<link rel="stylesheet" type="text/css" href="<?php echo WEB_ROOT;?>/admin/mos-css/mos-style.css" /> <!--pemanggilan file css-->
<link rel="stylesheet" type="text/css" href="<?php echo WEB_ROOT;?>/admin/include/mos-css/jquery.multiselect.css" /> 



<?php



$n = count($script);



for ($i = 0; $i < $n; $i++) {



  if ($script[$i] != '') {



    echo '<script language="JavaScript" type="text/javascript" src="' . WEB_ROOT. 'admin/library/' . $script[$i]. '"></script>';



  }



}



?>



<!--<script src="<?php echo WEB_ROOT;?>ckeditor/ckeditor.js"></script>-->



<script> 



var a=navigator.onLine; 



if(a){ 



// alert('online'); 



}else{



	window.location='login.php'; 



}



</script>







<style>



#leftBar ul li a {



    padding: 10px 10px 10px 25px !important;



}



</style>







</head>







<body>



<div id="header">



	<div class="inHeader">



		<div class="mosAdmin">



                     <?php $name =$_SESSION['user_name']; ?>



		Hello, <?php echo $name; ?><br>



	<div class="clear"></div>



	</div>



</div>







<div id="wrapper">



	<div id="leftBar">



	<ul>



       <p>&nbsp;</p>



       <?php if(isset($_SESSION['user_level']) && $_SESSION['user_level']=='0'){?>



			<li> <a href="<?php echo WEB_ROOT; ?>admin/">Home</a></li>



            <li><a href="<?php echo WEB_ROOT; ?>admin/formdata/">Part1(Applications Received)</a></li>



			<li><a href="<?php echo WEB_ROOT; ?>admin/approveddata/">Pre-Qualifying Applications</a></li>



			<li><a href="<?php echo WEB_ROOT; ?>admin/bidding/">Part2(Financial Qualification)</a></li>

      <li><a href="<?php echo WEB_ROOT; ?>admin/publish/">Publish Role</a></li>



       <?php } else if($_SESSION['user_level']=='2') {?>



            <li> <a href="<?php echo WEB_ROOT; ?>admin/">Home</a></li>
            <li><a href="<?php echo WEB_ROOT; ?>admin/user/">User</a></li>
            <li><a href="<?php echo WEB_ROOT; ?>admin/area/">Area / Location</a></li>
            <li><a href="<?php echo WEB_ROOT; ?>admin/formdata/">Part1(Applications Received)</a></li>
            <li><a href="<?php echo WEB_ROOT; ?>admin/bidding/">Part2(Financial Qualification)</a></li>
          <?php } else { ?>
            <li> <a href="<?php echo WEB_ROOT; ?>admin/">Home</a></li>


          <li><a href="<?php echo WEB_ROOT; ?>admin/qualification/">Qualification</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/state/">StateAdd</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/city/">District</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/user/">User</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/formdata/">Part1(Applications Received)</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/bidding/">Part2(Financial Qualification)</a></li>



          <li><a href="<?php echo WEB_ROOT; ?>admin/report/">Access Log</a></li>
          <?php }  ?>

            <li><a href="?function">Logout</a></li>



            <p>&nbsp;</p>



          <p>&nbsp;</p>



          <p>&nbsp;</p>



          <p>&nbsp;</p>



          



	</ul>   



	</div>



	<div id="rightContent">



            <table width="100%" border="0" cellspacing="0" cellpadding="20">



        <tr>



          <td>



            <?php require_once $content;?>



          </td>



        </tr>



        </table>







	</div>



<div class="clear"></div>



<div id="footer">



	



</div>



</div>



</div>



<script>



    $('#fromdate, #todate').datepicker({



    dateFormat: 'dd-mm-yy'



    });



</script>



</body>



</html>