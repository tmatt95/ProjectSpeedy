
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import BetTabs from "../../../Components/Bet/BetTabs";
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

describe(`The bet tabs component`, () =>
{
  it("renders correctly when status is created", () =>
  {
    let dummyBet: IBet = { name: "Bet Name", description: "Bet description", status: "Created", isLoaded: true, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
    act(() =>
    {
      render(<BetTabs bet={dummyBet} />, container);
    });

    expect(container).not.toBeNull();
    if (container !== null)
    {
      expect(container.getElementsByClassName("nav-link").length).toBe(1);
    }
  });

  it("renders correctly when status is in progress", () =>
  {
    let dummyBet: IBet = { name: "Bet Name", description: "Bet description", status: "In Progress", isLoaded: true, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
    act(() =>
    {
      render(<BetTabs bet={dummyBet} />, container);
    });

    expect(container).not.toBeNull();
    if (container !== null)
    {
      expect(container.getElementsByClassName("nav-link").length).toBe(2);
    }
  });

  it("renders correctly when status is finished", () =>
  {
    let dummyBet: IBet = { name: "Bet Name", description: "Bet description", status: "Finished", isLoaded: true, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
    act(() =>
    {
      render(<BetTabs bet={dummyBet} />, container);
    });

    expect(container).not.toBeNull();
    if (container !== null)
    {
      expect(container.getElementsByClassName("nav-link").length).toBe(3);
    }
  });
});