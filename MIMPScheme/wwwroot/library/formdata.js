// JavaScript Document
function checkAddForm()
{
	with (window.document.frmAddProduct) {
		if (isEmpty(txtName, 'Enter name')) {
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

function detail(id,formname)
{
    window.location.href = 'index.php?view=detail&id=' + id+'&type='+formname;
}

function submitImportForm()
{
	window.document.frmAddProduct.submit();
}

function resetForm(){
    $('#state').val('');
    $('#city').val('');
    $('#ref_num').val('');
    $('#fromdate').val('');
    $('#todate').val('');
    $('#formdata').val('all');
    document.myFilterForm.submit();
}

function GetCityByState(state, city){
    console.log(city);
    elementId = '#city';
    var listCity = '<option value="">Choose District.....</option>';
    $.ajax({
        url: '../../getcity.php?state='+state,
        type: "GET",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if(data.id != ''){
                $.each(data, function (key, value) {
                    listCity += '<option value="' + key + '" '+((city==key)?'selected':'')+'>' + value + '</option>';
                });
            }
            $(elementId).html(listCity);
        }
    });
}


$(document).ready(function(){
    $( "#fromdate" ).datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true});
    $( "#todate" ).datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true});
});