--Object1's code
Object1 = class('Object1')

function Object1:draw()
	love.graphics.print("x = " .. self.x .. ", y = " .. self.y,self.x,self.y)
	love.graphics.print("obji = " .. self.i .. " k = " .. self.k,self.x,self.y+16)
end

function Object1:initialize(x,y,id)
	self.x = x
	self.y = y
	self.id = id
	self.tp = "Object1"
end

return Object1