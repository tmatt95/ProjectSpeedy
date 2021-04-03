import { screen } from '@testing-library/react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { CardGrid, CardItem } from '../../Components/CardGrid';
import
{
  BrowserRouter as Router,
} from "react-router-dom";

let container: HTMLDivElement | null;

beforeEach(() => {
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

describe(`The card grid component`, () => {
  it('can render a grid with one item in', () => {
    act(() => {
      let cardData = new Array<CardItem>();
      cardData.push({
        address: "address",
        name: "Card Name"
      });
      let data = {
        data: cardData,
        AddNewClick: () => {}
      };
      ReactDOM.render(
        <Router>
          <CardGrid {...data} />
        </Router>,
        container);
    });
    const linkElement = screen.getByText(/Card Name/i);
    expect(linkElement).toBeInTheDocument();
  });
});
