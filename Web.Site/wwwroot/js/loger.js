
var imgCarry = "<img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAYAAACpSkzOAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAMXSURBVEhLxZbLK7RxFMe/Mw0iI5eYQkOWrrlkITaGsnApUWKn2GBB2FlIKTV/AXZYWCgL1JQNZYWdJCVmUFKS+915f+c8P5fHeLzv6528n5qec87vcub3POfys5ECP4Dt4OCAqqurUV9fj4GBAW0G9vf3sbW1BYfDAbvdjoeHB0REROD6+hpxcXEoLi7WM4HV1VUZLykp0ZZPSEpKouXlZT4Vtba20srKCh+Surq6yO12U05ODsXGxlJlZaXMaW5ulifT0NAgcllZGZWWlorc0tIiYx9BQkICra+vy6Ty8nJyuVwy0NHRQbOzs7S4uEj9/f1iy8zMlCfPzcrKor6+PtHf09bWRunp6Vp7A5ubm5SRkUE9PT30+PhIqampMtDZ2UkTExM0NzcnG+fn51N2draMsc5/xIqamhoaGRnRmoHxDjTHx8eUkpIiMjsaGxsTeW1tjdR3oN3dXdHZkfpWIn/G7e0tRUdHa83Arha9onQtGTw/P8uzsLAQRUVFUK9EdCYyMlJLwXDQhIeHm/YzOQolNzc3sNlsWvsHR0tLS1oKZmZmJijUv+2oqqoKT09PWnvj7OwMKuwxPz+vLQbfdjQ1NSXJPDg4iJ2dHUnuoaEhqJzD9PQ0rq6u4Pf7cXh4KPNNJUhFHQoKCqCqBVTCQiUr2tvb9egb/O45OPij39/fS2VIS0vDyckJEhMTxdnl5SXCwsI4fXB0dASHXvvX8OYveDwe+Hw+eL1eicympiY9AuTm5kr0hSTqzs/PUVFRgeHhYVxcXGirAZ+YCYmj8fFxdHd3Y3JyEnV1ddpqJiSO8vLyxEFtbS0XaW01ExJHf8L/d8QhzA0vFPA+pp24iL5ECXfM90WRq4BqI1ozYP3j7yO87u7uzpywbFRdVdo1f9Tt7W09AiQnJ0OVf3EeHx+P09PToGofFRX1WgkYXsNJ63Q6pTV8irozSAvf2NigxsbG1y77FYFAwHKNpSO+M4yOjmqNKCYmRkvWfLXG0hF3VHVs2tvbI1VSLC8d7/lqjaUjZmFhQS4hvb292vJ7rNb80AUS+AWpo2q3agmQVgAAAABJRU5ErkJggg==' />";

var imgAndroid = "<img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAYAAACpSkzOAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAUCSURBVEhLjZYJUNRVHMe/HAvKGQtyLKeCxKUFEscMqZBHjU6jeQxNosw44SCIZIfVUFNNNVMzFuMgZZF0zjCGE5UWKUeaFIhARByiBEa0HAu7y+qyEOzr/d7+l2E5/TA/eO/9vr/fe/93YsU4uAdKzpXh85Kz6P2nT9T9/XyxP3UvUnftEPUloY4WY0StZiHRCWznvnRWW9/AVMMjwqhMbeQjzVIs2tFdvZ55hkSy8opqqWUu5RVVzItrSLsYi07d7gMHkRQfj9zDGSj4+AzKzv8Aa2tr4aOwlPXr8dKxI8gvPI2rdfUo/axI+OaFOpoPmg5F2FqpxljS1u3svYIPWV1DE7vGjcqxGzZJXia0i02haXjzcLHqMpIS4qUaMKxWY/+Te6DVaqHhlpa6B/oxg+QF18bhUvVlqTaXBTtSDQ8jgO8sghmNmJj4D1ZWVkjLyEbaoWxeBjdrlH77PaamphAUGIgh1bDQz4et9Hea8spq6HQ6NDb/ATuZvWijRdTrxyB3c0NxYb5oc5fLMWYYw6BKJdZLf1cvYr4u+w7Ozs549JFkoTNjsRnePVmAktIyxEY/iJ6/e9HV3QM7OxlWB69CS2sbCk+8A0cHB6HluwyHnz2O+Nh1aG3vENMYvDIIQQH+uN70O1J378ALOdlCS1h0lLx9J95+NQ+JcevwwSef4l/lAJ7JysCPl6pQ39gE5cCg+FqCRu3j5YmHYqLx2OYUvF/4ERTeXsg8mI7frjXg5TfeRPX5b4SWsJg6mUyGO3wKiDGDQRhN11N7dwlbjDH+RaQnKAflmsmszcBgTas8D9rRUW46jEpGZWqbD1MOy+Np0dHk5BT/bRRlJ0dHscuI27294KcfAZHRCIiKMRkvUxv5CNJSjAmjlGsGtEZm6hoamWdwBHMLDBXXytHjeZJnaUhLMRRLOSjXTCzWKI4v7Latm7AlJRn9g4NiM9wrdnZ2ePFYDrw9Pflhrxa5ZjLnwE7yw+fk5MS38XIY+UEl+pT9CI1JRFTiBqxJ3CiMytRGPoK0FEOxlGM2cw4srSEFGY0MMluTW+HjhXNfnIGVteVGYVxDPoK0FCMGZ7kPBAteQbY8sKmlRdxrVvxnTWQ4osLDLIzayEca0lLMQizosbGxgVqtRUT8w1jh7g4ffhh9fXzg4uwk/KO6O3zalFD2D/B7cQR+vgoRsxBzOrK1tRHXDk3BE49vw/Jl9mi/cRNH+ZvU0to+fXZcXVywln8R3QgRYav5gR0XMRRLOWZj0VHnrS7c7OrGz1dqxHTI5W7IfjoX3qFRkPEE4aGhWOEhF9oh1QhOFRXjq7Ol6O/8E2+dyEd3z23c5+qKW391o63jBh/A/UJLWKzRvowsuHGhWqNBW2cnDIZxODo6oK+jGdW887rrDai6clUYlamNfKQhLcVQrKuzC9Izc6SsEtJ5YlqdjnmsDJNqjBUWFbPn8l6TaoyFxCTwZ8ko1ZgoU5sZ0lKMGY9VYWyU5zQz/UW/1tYhLjZGqoE/dBN8EKby4NAQNHykdM08/8rrwqis0Yxyn0poSEsxZujA1tTWS7UZU3fhp0psSd6AA5lH8CWfdz4IyUM3+Tj8FAqExyWhgj/XZFT2U3hP39gExVBsOs+xOXkjLlyskDwzNoNyYIC/rlVYZm+PhqZm8aLmZh0SvkB/PzT9UinKCxHg74v8U6fhwG8H+toa/l/RA1GRkhf4H8X0pO4PwzcfAAAAAElFTkSuQmCC' />";

var imgShip = "<img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAVCAYAAACpF6WWAAAABHNCSVQICAgIfAhkiAAAAAFzUkdCAK7OHOkAAAAEZ0FNQQAAsY8L/GEFAAAACXBIWXMAAA7EAAAOxAGVKw4bAAACYklEQVQ4T42Vy2oqQRCGSw1eoqJRcOcraEAkEiSQheBlI4JbwVdQQ4KvkGhwoW+guEyyyTYQJYKGKO7VpYKokI1ZmDpTddo5Xmb0fNDUdHX1b01X9Qh4hOfnZ3S73QgAbF9eXsSKOgdFb25uWGx33N7eighlVEXf39/RbDaziMPhwLu7Ozw7O+O5xWLBRqMhIvdRFc3n83Jmr6+v7CO79tG6GlopQJGTkxPxBDCbzdhOp1O2xOb6HkJ8j16vh3a7nbPS6XR4dXXFluY2mw37/b6I3Odgocrlsvy6m6NSqYgIZQ6KEs1mE/V6PYsZjUaeH+OoKHF5ecmiZP8HVdHhcIiRSATPz8/lTMnSPBqN4mg0EpH7aFqtFj49PYHBYIDVagVSESCXy0Emk4HHx0dJS5lsNgsPDw9wf38P39/foNVq4efnB+LxOIDL5ZILsB4fHx84Ho85I6VMY7EYTiYTjtvdy3pSv205pYxxPp+LF0H8/f1Fv9/Pa2RpvmaxWHD85n4qpjadTkvPf/n8/ITlcglSfwoPgEaj4dciyNJ8DR0VxbfbbeEBSKVSAF9fX/KvXF9fixy28Xg8vE5WCboYa41ut4tUHAyFQrKzWCyK0H8cEpWKJe8Nh8NIetxSb29vfBZqwl6vl/1kNykUCvIek8nEXzZC7tNqtYrSeclB1OjSGXPRfD4f+8hScTqdDl5cXMixtK9erwulneav1Wp4enoqB9MIBALodDr5mSzNN9etVuuWILF3o+jjm0gktjaqjWQyyb26i+I1pV4cDAZ8tpTZ+h+AvvjBYBBLpRJfY2UQ/wCif2TiATOUZAAAAABJRU5ErkJggg==' />";

var imgDock = "<img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAYAAACpSkzOAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAANySURBVEhLvZVpSFRRFMf/894447jNmDq2abQofZFc2ij6kIZYZiVEOxFFQSD1JSMiGop2/VAhBEX7pmJECBXUFBFFm0sEYWB+yFEzscaaUUfnzXTP9Za+mRczBvWDy7vLO+f/7r3nnKfzM/AfkMTzn/PfhIKO7kT5aTywP4Ys66HTickwIU+K4kV+3kKU7tohZgUk9ItjZSf9sinRHzMmxW+ISf6rRrbk43j5SeF1CNWOFi0uxstXb1CyfRvychfA4xkQK+FhNBpgf/QUFWfOYs7smXh477ZYCbgjvV4PSZLQ09MDR1sH2ttH18iGbMkH+RqJ7kZljb+3rw9bNm3AkuWrUV/fyHdCc4Ho2KVFGo283+/x0LHz/kiiTCa+s+zsTNy9U4Xzl67xOV0sO9MfX78woz4UFa/Hs+cvMGtmFlJTJsLr9QpzJsK+cnBwEI8eP+Xj3IULEBERAb/Px8cE7eJTqwOv3zRg/ry5qL19nX2cCbFjrEDq1Ax+icTSFWt5/07tXT7Wwjoxnbc/Qbbkg3wR1CcNzTxyu3tFT42TnT/tihr1tfiT7agT1pqUyNtokVodbRhwdcLnU8TU0KVrYTGb0fTuFW/U12KkLfkk36Qh+fq7efRIkoxBdvn0niz/fWUiW/JBvsgn+SYNVcIWFK1CXV0DEhLiWYga2RcNR1Q4UP54WNh3d39DTk4W7tdWixW200Chxsa36O/3wN2rfamhiI6KQmSkEZmZM1RC6vRl/HC5cNC2F+vWrFSdt6IocDrVkWaxmNlRyWLE6yZYAcD+A0fEzDBBQl6vgunpaWhubsG5C1d4VpOIxWJB2bED4q0hSvfYmLiTi1El2bp5I7clH4EECXHYRuiurl68DENMHM+bCePHBglVVtegrf0zrxADru/InpGBtPRpYlWNthBj+bJC6JkDqlsKC4qY6GixMszRQza43G7IPAgGULg4H++bPohVNUFCRoMBD+1PeH/y5En8SVAE3qy6JUZDmM1xiI+3iBG4CNmSj0BUUZdXUIy6+gZIOokXVIUlnNZ5a6HXy2xnMi+sPr8POdlZsN8f/h+phEp27kbzxxZ0dHTiS1cXKzVJSE1VV3EteNX+5PhtM25cMqZNnYKKUyfEGwwSCmSf7bBfikzgz3AJZaNZayhck61JqhwJRSgb1dH9O4CfNUFOOoAN86cAAAAASUVORK5CYII=' />";


function logAndroid(android) {

    var loger = $("#logs");


    loger.append("<div id='log.android." + android + "' class='border-top'>" + imgAndroid + "  Android." + android + "</div>");
}

function logAndroidToShip(android, ship) {

    var loger = $("#logs");

    var date = new Date();

    var tmillis = sec2time(date);

    loger.append("<div class='border-top'>[" + tmillis + "]Android." + android + imgAndroid + " : " + imgShip + " Ship." + ship + "</div>");
}

function logAndroidToDock(android, dock) {

    var loger = $("#logs");

    var date = new Date();

    var tmillis = sec2time(date);

    loger.append("<div class='border-top'>[" + tmillis + "]Android." + android + imgAndroid + " : " + imgDock + " Dock." + dock + "</div>");
}

function logDock(dock) {

    var loger = $("#logs");

    loger.append("<div class='border-top'>" + imgDock + "  Dock."  + dock + "</div>");
}

function logShip(ship) {

    var loger = $("#logs");

    loger.append("<div class='border-top'>" + imgShip + "  Ship." + ship + "</div>");
}

function logAndroidLoadGoods(android, goods, ship) {

    var loger = $("#logs");

    var date = new Date();

    var tmillis = sec2time(date);

    loger.append("<div class='border-top'>[" + tmillis + "]Android." + android + imgAndroid + " : Load : " + imgCarry + " "
                + goods + " Goods. Ship : " + imgShip + " " + ship + "</div>");
}

function logInitOrder(order) {

    var loger = $("#logs");

    loger.append("<div><b>" + order.type + " : " + order.num + "</b></div>");
}

function sec2time(date) {

    var minut = date.getMinutes();
    var seconds = date.getSeconds();
    var millis = date.getMilliseconds();


    return minut + ":" + seconds + "." + millis;
}

