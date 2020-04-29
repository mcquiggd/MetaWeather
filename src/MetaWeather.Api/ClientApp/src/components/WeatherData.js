import React, { Component } from 'react';
import ReactPullToRefresh from 'react-pull-to-refresh';
import authService from './api-authorization/AuthorizeService'
import { Log } from 'oidc-client';

export class WeatherData extends Component {
  static displayName = WeatherData.name;

  constructor(props) {
    super(props);
      this.state = { locations: [], loading: true };
      this.handleRefresh = this.handleRefresh.bind(this);
    }

  componentDidMount() {
    this.populateWeatherData();
    }

    async populateWeatherData() {
        console.log('refreshing');
        const token = await authService.getAccessToken();

        const response = await fetch('api/weather', {
            method: 'post',
            body: JSON.stringify({ woeId: 44544 }),
            headers: !token ? { 'Content-Type': 'application/json' } : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        });

        const data = await response.json();
        this.setState({ forecasts: data.forecasts, loading: false });
    }

    handleRefresh(resolve, reject) {
        let self = this;        
        setTimeout(function () {
        self.populateWeatherData() ? resolve() : reject();
        }, 500);
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
            <ReactPullToRefresh
                onRefresh={this.handleRefresh}
                className="your-own-class-if-you-want"
                style={{
                    textAlign: 'center'
                }}>
                <h3>Pull down to refresh</h3>
                {contents}
            </ReactPullToRefresh>
      </div>
    );
  }
}
