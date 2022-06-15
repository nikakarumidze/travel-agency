import React from 'react';
import Typography from '@mui/material/Typography';
import {
  Button,
  Container,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from '@mui/material';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import classes from './Search.module.scss';

import { useState } from 'react';

const Search = () => {
  const [bedNumber, setBedNumber] = useState(1);
  const [dateRange, setDateRange] = useState([null, null]);
  const [location, setLocation] = useState();
  const [startDate, endDate] = dateRange;

  const bedNumberChangeHandler = (event) => {
    setBedNumber(event.target.value);
  };

  const locationChangeHandler = (event) => {
    setLocation(event.target.value);
  };

  const searchHandler = () => {
    // If no Location provided, location will be nearest of user
    if (startDate === null) {
      const today = new Date();
      console.log(today);
      // If no date provided, the start date will be today.
    }
    console.log(bedNumber, location, startDate, endDate);
    // Fetching info and redirecting to search page
  };

  return (
    <Container>
      <Typography component='h1' variant='h4' sx={{ mb: 5 }}>
        Find Apartments
      </Typography>
      <Container sx={{ display: 'flex', justifyContent: 'center' }}>
        <FormControl sx={{ width: 70, p: 1 }}>
          <InputLabel id='bed'>Beds</InputLabel>
          <Select
            labelId='bed'
            defaultValue={1}
            onChange={bedNumberChangeHandler}
          >
            <MenuItem value={1}>1</MenuItem>
            <MenuItem value={2}>2</MenuItem>
            <MenuItem value={3}>3</MenuItem>
            <MenuItem value={4}>4 +</MenuItem>
          </Select>
        </FormControl>

        <TextField
          label='Search Location'
          sx={{ width: 176, m: 1 }}
          onChange={locationChangeHandler}
        />
        {/* Fetch Locations, and store as options */}

        <FormControl sx={{ m: 1 }}>
          {!startDate && (
            <InputLabel sx={{ mb: 0 }}>Check in - Check out</InputLabel>
          )}
          <DatePicker
            selectsRange={true}
            startDate={startDate}
            endDate={endDate}
            onChange={(update) => {
              setDateRange(update);
            }}
            isClearable={true}
            className={classes.datepicker}
          />
        </FormControl>

        <Button variant='contained' sx={{ m: 1 }} onClick={searchHandler}>
          Search
        </Button>
      </Container>
    </Container>
  );
};

export default Search;
