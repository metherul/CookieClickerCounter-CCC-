 $("#login-button").click(function(event){
		 event.preventDefault();
	 
	 $('form').fadeOut(500);
	 $('.wrapper').addClass('form-success');
	 document.getElementById("headerTest").innerHTML = "Thanks";
	 
	 document.getElementById("formName").submit();
});