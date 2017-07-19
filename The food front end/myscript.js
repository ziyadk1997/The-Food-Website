

	
	
	function initializeOrderList() 
	{
    var input, filter, ul, li, a, i;
    input = document.getElementById("orderSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("orderSetupItems");
	ul.style.display="";
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
	
	function initializeItemList() 
	{
    var input, filter, ul, li, a, i;
    input = document.getElementById("ItemSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("ItemSetupItems");
	ul.style.display="";
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
  
  
  
  
  $("#ItemSetupItems li a ").click(function()
    {
	  $("#ItemSetupItems").hide();
	  $("#ItemSetupFilterList").val(this.innerText);
	  var res = this.innerText;
    });
	
	 $("#orderSetupItems li a ").click(function()
    {
	  $("#orderSetupItems").hide();
	  $("#orderSetupFilterList").val(this.innerText);
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
			var btn=document.createElement("BUTTON");
			btn.innerHTML=name+"<br>";
			btn.innerHTML += $("#timepicker1").val();
			btn.innerHTML += "<br>";
			btn.innerHTML += $("#orderSetupFilterList").val();
			btn.setAttribute("class","btn btn-primary header orders");
			var table=$("#home_table");
			table.append(btn);
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
  
 
  
  
  
  
  