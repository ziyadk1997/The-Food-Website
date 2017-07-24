function displayBroadcasts (broadcasts)
{
    
    $("#home_table").empty();
	for(var i=0;i<broadcasts.length;i++)
	{
		var resname=broadcasts[i].Restaurant.Name;
		var deadline=broadcasts[i].Deadline;
		var uniqueId = broadcasts[i].BroadcastID;
        var btn = "<button unique_id = \"" + uniqueId + "\" restaurantid = \"" + broadcasts[i].Restaurant.RestaurantID + "\" class=\"btn btn-primary header orders single_order\">" + resname + "<br>" + deadline + "</button>";
		$("#home_table").append(btn);
	}
}
function displayHistory(broadcasts)
{
	for(var i=0;i<broadcasts.length;i++)
	{
		var uniqueId = broadcasts[i].BroadcastID;
		var resname=broadcasts[i].Restaurant.Name;
		var deadline=broadcasts[i].Deadline;
		var btn = "<button unique_id = \"" +uniqueId + "\" class=\"btn btn-primary header pay\">" + resname + "<br>" + deadline +"</button>";
		$("#history").append(btn);
	}
}