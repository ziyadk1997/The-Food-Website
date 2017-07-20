function displayBroadcasts (broadcasts)
{
	for(var i=0;i<broadcasts.length;i++)
	{
		var name=broadcasts[i].User.Name;
		var resname=broadcasts[i].Restaurant.Name;
		var deadline=broadcasts[i].Deadline;
		var uniqueId = broadcasts[i].BroadcastID;
		var btn = "<button unique_id = \"" +uniqueId + "\" class=\"btn btn-primary header pay\">" + name + "<br>" + resname + "<br>" + deadline +"</button>";
		$("#home_table").append(btn);
	}
}
function displayHistory(broadcasts)
{
	for(var i=0;i<broadcasts.length;i++)
	{
		var name=broadcasts[i].User.Name;
		var uniqueId = broadcasts[i].BroadcastID;
		var resname=broadcasts[i].Restaurant.Name;
		var deadline=broadcasts[i].Deadline;
		var btn = "<button unique_id = \"" +uniqueId + "\" class=\"btn btn-primary header pay\">" + name + "<br>" + resname + "<br>" + deadline +"</button>";
		$("#history").append(btn);
	}
}