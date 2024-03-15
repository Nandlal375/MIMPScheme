// JavaScript Document

function addStateUserForm()

{

	

		/*} else if(validPassword(txtPassword)){

			return;*/

	with (window.document.frmAddUser) {

		if (isEmpty(state, 'Please select state')) {

			return;

		}else if (isEmpty(txtUserName, 'Enter user name')) {

			return;

		} else if (isEmpty(txtPassword, 'Enter password')) {

			return;

		} else {

			var strPassword = $('#txtPassword').val();

            var strMD5 = sha256(strPassword);

			$('#txtPassword').val(strMD5);

			submit();

		}

	}

}

function checkAddUserForm()

{

	

		/*} else if(validPassword(txtPassword)){

			return;*/

	with (window.document.frmAddUser) {

		if (isEmpty(txtUserName, 'Enter user name')) {

			return;

		} else if (isEmpty(txtPassword, 'Enter password')) {

			return;

		} else if (isEmpty(txtname1, 'Enter first officer name !')) {

			return;

		} else if (isEmpty(txtemail1, 'Enter first officer email !')) {

			return;

		} else if (isEmpty(txtdesignation1, 'Enter first officer designation !')) {

			return;

		} else if (isEmpty(txtname2, 'Enter second officer name !')) {

			return;

		} else if (isEmpty(txtemail2, 'Enter second officer email !')) {

			return;

		} else if (isEmpty(txtdesignation2, 'Enter second officer designation !')) {

			return;

		} else if (isEmpty(txtname3, 'Enter third officer name !')) {

			return;

		} else if (isEmpty(txtemail3, 'Enter third officer email !')) {

			return;

		} else if (isEmpty(txtdesignation3, 'Enter third officer designation !')) {

			return;

		} else {

			var strPassword = $('#txtPassword').val();

            var strMD5 = sha256(strPassword);

			$('#txtPassword').val(strMD5);

			submit();

		}

	}

}


function checkModifyUserForm()

{

	

		/*} else if(validPassword(txtPassword)){

			return;*/

	with (window.document.frmAddUser) {

		if (isEmpty(txtUserName, 'Enter user name')) {

			return;

		}  else if (isEmpty(txtname1, 'Enter first officer name !')) {

			return;

		} else if (isEmpty(txtemail1, 'Enter first officer email !')) {

			return;

		} else if (isEmpty(txtdesignation1, 'Enter first officer designation !')) {

			return;

		} else if (isEmpty(txtname2, 'Enter second officer name !')) {

			return;

		} else if (isEmpty(txtemail2, 'Enter second officer email !')) {

			return;

		} else if (isEmpty(txtdesignation2, 'Enter second officer designation !')) {

			return;

		} else if (isEmpty(txtname3, 'Enter third officer name !')) {

			return;

		} else if (isEmpty(txtemail3, 'Enter third officer email !')) {

			return;

		} else if (isEmpty(txtdesignation3, 'Enter third officer designation !')) {

			return;

		} else {

			var strPassword = $('#txtPassword').val();
			if(strPassword!=""){
				var strMD5 = sha256(strPassword);

				$('#txtPassword').val(strMD5);
			}
           

			submit();

		}

	}

}

function checkModifyUserForm1()

{

	/*

	} else if (!isEmpty(txtPassword, '')) {

			if(validPassword(txtPassword)){

				return;

	*/

	with (window.document.frmAddUser) {

		if (isEmpty(txtUserName, 'Enter user name')) {

			return;		

		}else {

			var strPassword = $('#txtPassword').val();

			var strMD5 = sha256(strPassword);

			$('#txtPassword').val(strMD5);

			submit();

		}

		/*} else {

			submit();

		}*/

	}

}



function addUser()

{

	window.location.href = 'index.php?view=add';

}


function addStateUser()

{

	window.location.href = 'index.php?view=addStateUser';

}


function changePassword(userId)

{

	window.location.href = 'index.php?view=modify&userId=' + userId;

}



function deleteUser(userId,strstatus)

{

	/*if (confirm('Delete this user?')) {

		window.location.href = 'processUser.php?action=delete&userId=' + userId+'&status='+strstatus;

	}*/

	window.location.href = 'processUser.php?action=delete&userId=' + userId+'&status='+strstatus;

}



function GetCityByState(state, city){

    

//    elementId = '#city';
    var elementId = '#disCityMultiSelect';

    
    $.ajax({

        url: '../../getcity.php?state='+state,

        type: "GET",

        cache: false,

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        success: function (data) {

            if(data.id != ''){
                city  = city.split(",");
                console.log(city);
                
                var listCity = '<select name="city[]" id="city" multiple="multiple">';
//                listCity += '<option value="" disabled="disabled">Choose District.....</option>';



                $.each(data, function (key, value) {

                    listCity += '<option value="' + key + '" '+((city.indexOf(key)!= -1)?'selected':'')+'>' + value + '</option>';

                });
                
                
                
                listCity += '</select>';
                $(elementId).html(listCity);
                $("select#city")
                  .filter(".single")
                  .multiselect({
                     multiple: false,
                     noneSelectedText: 'Please select a radio',
                     header: false
                  })
                  .end()
                  .not(".single")
                  .multiselect({
                     noneSelectedText: 'Please select a checkbox'
                  });
      
      
      

            }
        }

    });
    
}