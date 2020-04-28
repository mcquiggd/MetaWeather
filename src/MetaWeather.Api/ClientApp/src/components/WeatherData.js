import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class WeatherData extends Component {
  static displayName = WeatherData.name;

  constructor(props) {
    super(props);
    this.state = { locations: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Weather State</th>   
            <th>Image</th>            
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
              <tr key={forecast.applicableDate}>
                  <td>{forecast.dayOfWeek}</td>
                  <td>{forecast.weatherStateName}</td>
                  <td><img src={forecast.weatherStateImageURL} alt={forecast.weatherStateName} style={{ height: 100, width: 100 }}/></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : WeatherData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel">Weather Forecasts</h1>
        <p>This component demonstrates fetching weather forecast data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const token = await authService.getAccessToken();

      const response = await fetch('api/weather', {
              method: 'post',
          body: JSON.stringify({ woeId: 44544}),              
          headers: !token ? { 'Content-Type': 'application/json'} : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
      });

    const data = await response.json();
    this.setState({ forecasts: data.forecasts, loading: false });
  }
}
