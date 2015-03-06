--Object0's code
Object0 = class('Object0')

function Object0:draw()
	love.graphics.print("x = " .. self.x .. ", y = " .. self.y,self.x,self.y)
	love.graphics.print("obji = " .. self.i,self.x,self.y+16)
end

function Object0:initialize(x,y,id)
	self.x = x
	self.y = y
	self.id = id
	self.tp = "Object0"
end

return Object0