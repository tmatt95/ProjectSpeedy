
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
  it("renders nothing when bet not loaded", () =>
  {
    let dummyBet: IBet = { name: "", description: "", status: "", isLoaded: false, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
    act(() =>
    {
      render(<BetTabs bet={dummyBet} />, container);
    });

    expect(container).not.toBeNull();
    if (container !== null)
    {
      expect(container.innerHTML).toBe("");
    }
  });

  it("renders 1 tab when status is created", () =>
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

  it("renders 2 tabs when status is in progress", () =>
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

  it("renders 3 tabs when status is finished", () =>
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

  it("renders nothing when it gets a status it is not expecting", () =>
  {
    let dummyBet: IBet = { name: "", description: "", status: "unknown", isLoaded: true, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
    act(() =>
    {
      render(<BetTabs bet={dummyBet} />, container);
    });

    expect(container).not.toBeNull();
    if (container !== null)
    {
      expect(container.innerHTML).toBe("");
    }
  });
});