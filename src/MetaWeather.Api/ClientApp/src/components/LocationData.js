import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class LocationData extends Component {
  static displayName = LocationData.name;

  constructor(props) {
    super(props);
    this.state = { locations: [], loading: true };
  }

  componentDidMount() {
    this.populateLocationData();
  }

  static renderForecastsTable(locations) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>WoeId</th>
            <th>Title</th>
            <th>Location Type</th>
            <th>Lattitude and Longitude</th>
          </tr>
        </thead>
        <tbody>
          {locations.map(location =>
            <tr key={location.woeId}>
              <td>{location.woeId}</td>
              <td>{location.title}</td>
              <td>{location.locationType}</td>
              <td>{location.latLong}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : LocationData.renderForecastsTable(this.state.locations);

    return (
      <div>
        <h1 id="tabelLabel" >Locations</h1>
        <p>This component demonstrates fetching location data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateLocationData() {
    const token = await authService.getAccessToken();

      const response = await fetch('api/location', {
              method: 'post',
              body: JSON.stringify({ cityName : 'Birmingham'}),              
          headers: !token ? { 'Content-Type': 'application/json'} : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
      });

    const data = await response.json();
    this.setState({ locations: data.locations, loading: false });
  }
}
