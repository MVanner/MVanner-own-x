function love.load()
    board = love.graphics.newImage("assets/board.png")
    player = love.graphics.newImage("assets/player.png")
    computer = love.graphics.newImage("assets/computer.png")
    scoreBar = love.graphics.newImage("assets/scoreBar.png")
end

function love.draw()
    love.graphics.draw(board, 0, 0)
    love.graphics.draw(player, 50, board:getHeight() / 2)
    love.graphics.draw(computer, 700, board:getHeight() / 2)
end