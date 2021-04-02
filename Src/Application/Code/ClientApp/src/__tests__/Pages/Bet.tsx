import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import { BreadCrumbItem } from '../../Components/BreadCrumbs';
import { IBet } from '../../Interfaces/IPage';
import {Bet} from '../../Pages/Bet';
import { BetService } from '../../Services/BetService';

let container: HTMLElement | null;

beforeEach(() =>
{
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() =>
{
  if (container !== null)
  {
    document.body.removeChild(container);
    container = null;
  }
});

describe(`The bet page`, () => {
  it('can render',async() =>
  {
    const bet: IBet = { name: "Name", isLoaded: true, description: "Description", successCriteria: "", status:"Created", timeCurrent:0, timeTotal:0 };
    jest.spyOn(BetService, "Get").mockReturnValue(Promise.resolve(bet));

    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; }
    }
    await act(async() => {
      ReactDOM.render(
        <MemoryRouter>
          <Bet {...pageData} />
        </MemoryRouter>, container);
    });
  });
});

