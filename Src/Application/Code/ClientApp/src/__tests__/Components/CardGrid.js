import { screen } from '@testing-library/react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { CardGrid } from '../../Components/CardGrid';
import
{
  BrowserRouter as Router,
} from "react-router-dom";

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The card grid component`, () => {
  it('can render a grid with one item in', () => {
    act(() => {
      let cardData = new Array();
      cardData.push({
        address: "address",
        name: "Card Name"
      });
      let data = {
        data: cardData
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
