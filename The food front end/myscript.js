

	function myFunction() 
	{
    var input, filter, ul, li, a, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("myUL");
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) 
	{
        a = li[i].getElementsByTagName("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
	
	}

 $(document).ready(function(){
	 
	 
  $("#history").hide();
  $("#history_select").hide();
  $("#home_select").hide();
  $("home").show();
  $("#add_btn").show();
  $("#check_btn").hide();
  $("#recipt").hide();
  $("#order_setup").hide();
  
   $(".orders td").click(function() {
        alert("You clicked my <td>!" + $(this).html() + 
              "My TR is:" + $(this).parent("tr").html());
        //get <td> element values here!!??
    });
  $("#history_btn").click(function()
	{	
			$("#history").show();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").hide();	
			$("#add_btn").hide();
			$("#check_btn").hide();
			$("#recipt").hide();
			$("#order_setup").hide();	
	});
	$("#details").click(function()
	{
			var details=prompt("please type any special comments you would like to include in the order");
	});
	$(".order_details").click(function()
	{
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").hide();	
			$("#add_btn").hide();
			$("#check_btn").hide();
			$("#recipt").show();
			$("#order_setup").hide();
	});
	$("#current_order_btn").click(function()
	{
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").show();
			$("#add_btn").show();
			$("#check_btn").hide();
			$("#recipt").hide();
			$("#order_setup").hide();			
	});
	$("#add_btn").on("click", function()
	{
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").hide();
			$("#add_btn").hide();
			$("#check_btn").hide();
			$("#recipt").hide();
			$("#order_setup").show();
	});
	$("#done").on("click", function()
	{
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").show();
			$("#add_btn").show();
			$("#check_btn").hide();
			$("#recipt").hide();
			$("#order_setup").hide();
			var name=prompt("Please enter your name");
	});
	$(".orders").click(function()
	{
			
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").show();
			$("#home").hide();
			$("#add_btn").hide();
			$("#check_btn").show();
			$("#recipt").hide();
			$("#order_setup").hide();
	});
	$(".pay").click(function()
	{
			
			$("#history").hide();
			$("#history_select").show();
			$("#home_select").hide();
			$("#home").hide();
			$("#add_btn").hide();
			$("#check_btn").hide();
			$("#recipt").hide();
	});
	$("#check_btn").click(function()
	{
			$("#history").hide();
			$("#history_select").hide();
			$("#home_select").hide();
			$("#home").show();
			$("#add_btn").show();
			$("#check_btn").hide();
			$("#recipt").hide();
			$("#order_setup").hide();
	});
  });
  
 
  
  
  
  
  