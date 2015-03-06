--Room0's code
Room0 = class('Room0')

function Room0:draw()
	for i = 1, table.getn(self.objects) do
		self.objects[i]:draw()
	end
end

function Room0:update()
	for i = 1, table.getn(self.objects) do
		if self.objects[i].tp == "Object0" then
			self.objects[i].i = self.objects[i].i+1
		end
	end
end

function Room0:initialize(objects)
	self.tp = "Room0"
	self.objects = {}
	local obj = require('Object0')
	table.insert(self.objects,obj(0,0,2))
	obj.i = 8
end

return Room0