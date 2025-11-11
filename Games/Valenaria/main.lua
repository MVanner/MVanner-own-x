function love.load()
    sti = require 'libraries/sti'
    gameMap = sti('maps/worldMap.lua')
end

function love.draw()
    gameMap:draw()
end