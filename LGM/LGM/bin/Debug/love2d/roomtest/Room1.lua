--Room1's code
Room1 = class('Room1')

function Room1:draw()
	for i = 1, table.getn(self.objects) do
		self.objects[i]:draw()
	end
end

function Room1:update()
	for i = 1, table.getn(self.objects) do
		if self.objects[i].tp == "Object0" then
			self.objects[i].i = self.objects[i].i+1
		end
	end
end

function Room1:initialize(objects)
	self.tp = "Room1"
	self.objects = {}
	local obj = require('Object0')
	table.insert(self.objects,obj(0,0,2))
	obj.i = 8
	local obj = require('Object1')
	table.insert(self.objects,obj(0,48,2))
	obj.i = 5
	obj.k = 7
end

return Room1