// JavaScript Document
function checkAddForm()
{
	with (window.document.frmAddProduct) {
		if (isEmpty(state, 'Select state')) {
			return;
		} else if (isEmpty(city, 'Select district')) {
			return;
		} else if (isEmpty(txtName, 'Enter name')) {
			return;
		} else {
			submit();
		}
	}
}

function add()
{
	window.location.href = 'index.php?view=add';
}

function modify(id)
{
	window.location.href = 'index.php?view=modify&id=' + id;
}

function deleteData(id)
{
	window.location.href = 'index.php?view=delete&id=' + id;
}

function submitImportForm()
{
	window.document.frmAddProduct.submit();
}

function GetCityByState(state, city){

    

    elementId = '#city';

    var listCity = '<option value="">Select District.....</option>';

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
                $.each(data, function (key, value) {

                    listCity += '<option value="' + key + '" '+((city.indexOf(key)!= -1)?'selected':'')+'>' + value + '</option>';

                });

            }

            $(elementId).html(listCity);

        }

    });

}