#include<stdio.h>
#include<string.h>
#include<pthread.h>
#include<stdlib.h>
#include<unistd.h>
#include<sys/types.h>
#include<semaphore.h>

/* define minimum value will use multi-thread */
#define STEP 100000

/* 345678 */
/* thread = 3 */
/* 1 -> 100000
   100001 -> 200000
   200001 -> 300000
   300001 -> 345678
*/

sem_t sem;
pthread_mutex_t pmt;
int gmaxnum = 0;
int gmaxcount = 0;
int threadcount = 0;

static int calvalue(unsigned int val)
{
  unsigned int res = val;
  if(val == 1)
    {
      return 0;
    }
  else if((val % 2) == 0)
    {
      res = (val/2);
    }
  else
    {
      res = (3*val + 1);
    }

  return res;
}

static int findmax(unsigned int start, unsigned int number, int *pnumm, int *pcoutm)
{
  int i;
  int countmax = 0;
  int count = 0;
  int nummax = 1;
  for(i = start; i <= number; i++)
    {
      {
	int val = i;
	count = 0;
	while((val = calvalue(val)) != 0)
	  {
	    count++;
	  }
	count++;
	if(count >= countmax)
	  {
	    countmax = count;
	    nummax = i;
	  }
      }
    }
  *pnumm = nummax;
  *pcoutm = countmax;
  return 0;
}

typedef struct _data
{
  unsigned int start;
  unsigned int end;
  int id;
}data;

static void *calmax(void *arg)
{
  data *pdata = (data*)arg;
  int start = pdata->start;
  int end = pdata->end;
  int id = pdata->id;
  int maxnum = start;
  int maxcount = 0;

  findmax(start, end, &maxnum, &maxcount);

  sem_wait(&sem);
  //pthread_mutex_lock(&pmt);
  /* update maxnum */
  if(gmaxcount <= maxcount)
    {
      /* update maxcount */
      gmaxcount = maxcount;
      gmaxnum = maxnum;
    }
  sem_post(&sem);
  //pthread_mutex_unlock(&pmt);
  threadcount++;
  return 0;
}

int main(int argc, char **argv)
{
  int number = 0;
  int count = 0;
  int num_thread = 0;
  data dat[100];
  sem_init(&sem, 0, 1);
  sscanf(argv[1], "%d", &number);
  if(number <= STEP)
    {
      /* only use one thread */
      int nummax = 0;
      int countmax = 0;
      findmax(1, number, &nummax, &countmax);
      gmaxnum = nummax;
      gmaxcount = countmax;
    }
  else
    {
      /* devide number thread base on number */
      num_thread = number / STEP;
      int i = 0;
      for(i = 0; i <= num_thread; i++)
	{
	  int end;
	  dat[i].start = 1 + i*STEP;
	  if(dat[i].start > number)
	    continue;
	  if(((i+1) * STEP) >= number)
	    {
	      dat[i].end = number;
	    }
	  else
	    {
	      dat[i].end = (i + 1)*STEP;
	    }
	  dat[i].id = i;
          pthread_t pthr;
          pthread_attr_t attr;
          pthread_attr_init(&attr);
          pthread_attr_setdetachstate(&attr, PTHREAD_CREATE_DETACHED);
          int res = pthread_create(&pthr, &attr, calmax, &dat[i]);
	  if(res < 0)
	    {
	      fprintf(stderr, "cannot create a new thread\n");
	    }
	}
    }
  while(threadcount != num_thread);
  printf("gmaxnum %d maxcount %d\n", gmaxnum, gmaxcount);
  return 0;
}
