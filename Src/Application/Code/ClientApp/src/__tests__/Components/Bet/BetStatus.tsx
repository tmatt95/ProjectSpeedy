
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import BetStatus from "../../../Components/Bet/BetStatus";
import { IBet } from "../../../Interfaces/IPage";

let container: HTMLDivElement | null = null;

beforeEach(() =>
{
  // setup a DOM element as a render target
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() =>
{
  // cleanup on exiting
  if (container !== null)
  {
    unmountComponentAtNode(container);
    container.remove();
    container = null;
  }
});

it("renders with or without a name", () =>
{
  let dummyBet: IBet = {name:"Bet Name", description:"Bet description", status:"Created", isLoaded:true, timeCurrent:0, timeTotal:0, successCriteria:""} as IBet;
  act(() =>
  {
    render(BetStatus(dummyBet), container);
  });

  expect(container).not.toBeNull();
  if (container !== null)
  {
    let elements: HTMLCollectionOf<HTMLHeadingElement> = container.getElementsByTagName("h2");
    expect(elements[0].innerHTML).toBe("Start Bet"); 
  }
});