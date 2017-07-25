function displayBroadcasts (broadcasts)
{
    
    $("#home_table").empty();
	for(var i=0;i<broadcasts.length;i++)
	{
		var resname=broadcasts[i].Restaurant.Name;
		var deadline=broadcasts[i].Deadline;
		var uniqueId = broadcasts[i].BroadcastID;
		var array = deadline.split('T');
		var time = array[1];
        var btn = "<button unique_id = \"" + uniqueId + "\" restaurantid = \"" + broadcasts[i].Restaurant.RestaurantID + "\" class=\"btn btn-primary header orders single_order\">" + resname + "<br>" + time + "</button>";
		$("#home_table").append(btn);
	}
}
function displayHistory(broadcasts)
{
    
    $("#history").empty();
    $("#history").append("<br>");
    $("#history").append("<br>");
	for(var i=0;i<broadcasts.length;i++)
	{
		var uniqueId = broadcasts[i].BroadcastID;
		var resname=broadcasts[i].Restaurant.Name;
		var deadline = broadcasts[i].Deadline;
		var array = deadline.split('T');
		var date = array[0];
		var time = array[1];
		var btn = "<button unique_id = \"" + uniqueId + "\" restaurantid = \"" + broadcasts[i].Restaurant.RestaurantID + "\" class=\"btn btn-primary header pay\">" + resname + "<br>" + time + "<br>" + date + "</button>";
		$("#history").append(btn);
	}
}