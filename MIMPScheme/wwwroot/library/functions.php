<?php

//include 'security.php';

define('PRIVATE_KEY','es1c0v65nuj07/o6-2o13');

define('VERSION','1.0');

define('MY_IP',$_SERVER['REMOTE_ADDR']);

//Function check for session flixation

function check(){

	if(isset($_SESSION['HTTP_USER_AGENT'])){

		if($_SESSION['HTTP_USER_AGENT'] != md5($_SERVER['HTTP_USER_AGENT'].PRIVATE_KEY.VERSION.MY_IP)){

			header('Location: ' . WEB_ROOT . 'admin/login.php');

			exit;

		}

	}else{

		header('Location: ' . WEB_ROOT . 'admin/login.php');

		exit;	

	}

}

function checkUserRole($role_id){

	if(isset($_SESSION['user_id'])){

		if(!in_array($_SESSION['user_level'],$role_id)){

			header('Location: ' . WEB_ROOT . 'admin/login.php');

			exit;

		}

	}else{

		header('Location: ' . WEB_ROOT . 'admin/login.php');

		exit;	

	}

}



function buildSecureKey(){

	//get the IP address of the current server

	$ip = $_SERVER['SERVER_ADDR'];

	

	//Add time (GMT) and private information to be private key
	$seconds=gmdate("s");  
	if($seconds<=30){
		$seconds=30;
	}else{
		$seconds=59;
	}

	$key = PRIVATE_KEY.','.gmdate("Y,m,d,H,i").','.$seconds.','.$ip.','.VERSION;

	

	//sha1-encrypt the key

	$key = sha1($key);

	

	//Build a 16 hexadecimal characters random string

	//$rand_salt = substr(md5(uniqid(rand(),true)),0,16);

	$rand_salt = substr(md5(PRIVATE_KEY),0,16);

	

	//and inject it into the key after the 18th character

	$secure_key = substr($key,0,18).$rand_salt.substr($key,-22);

	

	//return the secure key

	return $secure_key;

}



function checkUser()

{

	// if the session id is not set, redirect to login page

	/*if (!isset($_SESSION['user_id'])) {

		header('Location: ' . WEB_ROOT . 'admin/login.php');

		exit;

	}*/

	check();

	

	// the user want to logout

	if (isset($_GET['logout'])) {

		doLogout();

	}

}



/*

	

*/

function doLogin()

{

	// if we found an error save the error message in this variable

	$errorMessage = '';

	



//  if(empty($_SESSION['letters_code']) || strcasecmp($_SESSION['letters_code'], $_POST['txt_letters_code']) != 0){
//
//  //if($_POST['txt_letters_code'] == ''){  		
//
//	$errorMessage = 'Wrong validation code!';
//
//}else{

	$userName = $_POST['txtUserName'];

	$password = $_POST['txtPassword'];
	$enc = hash('sha256','Admin@123');

//	 echo $enc.'<br><br>';


//	  $_SESSION['user_id'] = $username; 

	// first, make sure the username & password are not empty

	if ($userName == '') {

		$errorMessage = 'You must enter your username';

	} else if ($password == '') {

		$errorMessage = 'You must enter the password';

	} else {

		// check the database and see if the username and password combo do match

		$sql = "SELECT user_id,user_name,user_level,user_password,state,city

		        FROM tbl_user 

				WHERE user_name = '$userName' and status=1 limit 0,1";

		$result = dbQuery($sql);

	

		if (dbNumRows($result) == 1) {

			$row = dbFetchAssoc($result);
//                        print_r($row);
			#hash('sha256', $combo)
//			$enc = hash('sha256',$row['user_password'].buildSecureKey());
			$enc = $row['user_password'];

//			echo $password.'===='.$enc;die;

			if($password===$enc){

				session_regenerate_id(true);

				$_SESSION['HTTP_USER_AGENT'] = md5($_SERVER['HTTP_USER_AGENT'].PRIVATE_KEY.VERSION.MY_IP);

				$_SESSION['user_id'] = $row['user_id'];

				$_SESSION['user_name'] = $row['user_name'];

				$_SESSION['user_state'] = $row['state'];

				$_SESSION['user_city'] = $row['city'];
				$_SESSION['user_level'] = $row['user_level'];

				dbQuery("INSERT INTO `tbl_log` (`username` ,`datetime` ,`ipaddress` ,`message`) VALUES ('".$userName."',CURRENT_TIMESTAMP , '".$_SERVER['REMOTE_ADDR']."', 'Login Successful');");

				// log the time when the user last login

				$sql = "UPDATE tbl_user 

						SET user_last_login = NOW() 

						WHERE user_id = '{$row['user_id']}'";

				dbQuery($sql);

	

				// now that the user is verified we move on to the next page

				// if the user had been in the admin pages before we move to

				// the last page visited

							//$row = dbFetchAssoc($result);

							

				if ($userName=='admin' || $row['user_level']==1) {

	//                            echo $userName;

	//                            echo'1';

	

					header('Location: '.WEB_ROOT.'admin/');

					exit;

				} else if ($row['user_level']==0){

	//                            echo $userName;

	//                                     echo'2';

					header("Location: ".WEB_ROOT.'admin/');

					exit;

				} else if ($row['user_level']==2){

	//                            echo $userName;

	//                                     echo'2';

					header("Location: ".WEB_ROOT.'admin/');

					exit;

				}

			}else{

				$errorMessage = 'Wrong username or password';

			}

			

		} else {

			dbQuery("INSERT INTO `tbl_log` (`username` ,`datetime` ,`ipaddress` ,`message`) VALUES ('".$userName."',CURRENT_TIMESTAMP , '".$_SERVER['REMOTE_ADDR']."', 'Login Failed');");

			$errorMessage = 'Wrong username or password';

		}		

			

	}

//}

	

	return $errorMessage;

}



/*

	Logout a user

*/

function doLogout()

{

	

	if (isset($_SESSION['user_id'])&&($_SESSION['user_name'])) {

		dbQuery("INSERT INTO `tbl_log` (`username` ,`datetime` ,`ipaddress` ,`message`) VALUES ('".$_SESSION['user_name']."',CURRENT_TIMESTAMP , '".$_SERVER['REMOTE_ADDR']."', 'Logout');");

		unset($_SESSION['user_id']);

        unset($_SESSION['user_name']);

		unset($_SESSION['HTTP_USER_AGENT']);

		//session_unregister('user_id');

        //session_unregister('user_name');

		//setcookie('PHPSESSID','',time()-3600);  

		session_destroy();

	}

		

	header('Location: '.WEB_ROOT.'admin/login.php');

	exit;

}





/*

	Create a thumbnail of $srcFile and save it to $destFile.

	The thumbnail will be $width pixels.

*/

function createThumbnail($srcFile, $destFile, $width, $quality = 75)

{

	$thumbnail = '';

	

	if (file_exists($srcFile)  && isset($destFile))

	{

		$size        = getimagesize($srcFile);

		$w           = number_format($width, 0, ',', '');

		$h           = number_format(($size[1] / $size[0]) * $width, 0, ',', '');

		

		$thumbnail =  copyImage($srcFile, $destFile, $w, $h, $quality);

	}

	

	// return the thumbnail file name on sucess or blank on fail

	return basename($thumbnail);

}



/*

	Copy an image to a destination file. The destination

	image size will be $w X $h pixels

*/

function copyImage($srcFile, $destFile, $w, $h, $quality = 75)

{

    $tmpSrc     = pathinfo(strtolower($srcFile));

    $tmpDest    = pathinfo(strtolower($destFile));

    $size       = getimagesize($srcFile);



    if ($tmpDest['extension'] == "gif" || $tmpDest['extension'] == "jpg")

    {

       $destFile  = substr_replace($destFile, 'jpg', -3);

       $dest      = imagecreatetruecolor($w, $h);

       imageantialias($dest, TRUE);

    } elseif ($tmpDest['extension'] == "png") {

       $dest = imagecreatetruecolor($w, $h);

       imageantialias($dest, TRUE);

    } else {

      return false;

    }



    switch($size[2])

    {

       case 1:       //GIF

           $src = imagecreatefromgif($srcFile);

           break;

       case 2:       //JPEG

           $src = imagecreatefromjpeg($srcFile);

           break;

       case 3:       //PNG

           $src = imagecreatefrompng($srcFile);

           break;

       default:

           return false;

           break;

    }



    imagecopyresampled($dest, $src, 0, 0, 0, 0, $w, $h, $size[0], $size[1]);



    switch($size[2])

    {

       case 1:

       case 2:

           imagejpeg($dest,$destFile, $quality);

           break;

       case 3:

           imagepng($dest,$destFile);

    }

    return $destFile;



}



/*

	Create the paging links

*/

function getPagingNav($sql, $pageNum, $rowsPerPage, $queryString = '')

{

	$result  = mysql_query($sql) or die('Error, query failed. ' . mysql_error());

	$row     = mysql_fetch_array($result, MYSQL_ASSOC);

	$numrows = $row['numrows'];

	

	// how many pages we have when using paging?

	$maxPage = ceil($numrows/$rowsPerPage);

	

	$self = $_SERVER['PHP_SELF'];

	

	// creating 'previous' and 'next' link

	// plus 'first page' and 'last page' link

	

	// print 'previous' link only if we're not

	// on page one

	if ($pageNum > 1)

	{

		$page = $pageNum - 1;

		$prev = " <a href=\"$self?page=$page{$queryString}\">[Prev]</a> ";

	

		$first = " <a href=\"$self?page=1{$queryString}\">[First Page]</a> ";

	}

	else

	{

		$prev  = ' [Prev] ';       // we're on page one, don't enable 'previous' link

		$first = ' [First Page] '; // nor 'first page' link

	}

	

	// print 'next' link only if we're not

	// on the last page

	if ($pageNum < $maxPage)

	{

		$page = $pageNum + 1;

		$next = " <a href=\"$self?page=$page{$queryString}\">[Next]</a> ";

	

		$last = " <a href=\"$self?page=$maxPage{$queryString}{$queryString}\">[Last Page]</a> ";

	}

	else

	{

		$next = ' [Next] ';      // we're on the last page, don't enable 'next' link

		$last = ' [Last Page] '; // nor 'last page' link

	}

	

	// return the page navigation link

	return $first . $prev . " Showing page <strong>$pageNum</strong> of <strong>$maxPage</strong> pages " . $next . $last; 

}



function encrypt($val = '') {

	if ($val != ''){

		return base64_encode($val);

	}

}



function decrypt($val = '') {

	if ($val != ''){

		return base64_decode($val);

	}

}


function getCityById($id){
    if(empty($id)){
        return '';
    }
    $sql = "SELECT name 
        FROM tbl_city
        WHERE id in ($id)
		ORDER BY name asc";

	$result = dbQuery($sql);

	$optHtml = '';

	while($row = dbFetchAssoc($result)) {

		$optHtml .= $row['name'].', ';

	}

	return rtrim($optHtml,', ');
}


function getCategory($catId = ''){

	$sql = "SELECT cat.id, cat.name

        FROM tbl_category as cat

		ORDER BY name asc";

	$result = dbQuery($sql);

	$optHtml = '<option value="">Select Category</option>';

	while($row = dbFetchAssoc($result)) {

		$optHtml .= '<option value="'.$row['id'].'" '.($catId == $row['id']?'selected="selected"':'').'>'.$row['name'].'</option>';

	}

	return $optHtml;

}



?>