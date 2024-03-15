// JavaScript Document
function checkAddForm()
{
	with (window.document.frmAddProduct) {
		if (isEmpty(state, 'Select state')) {
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