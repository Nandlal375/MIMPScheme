// JavaScript Document
function checkAddForm()
{
	with (window.document.frmAddProduct) {
		
			submit();
		
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

function submitImportForm()
{
	window.document.frmAddProduct.submit();
}