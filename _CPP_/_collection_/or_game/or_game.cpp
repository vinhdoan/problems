//qscqesze
#include <cstdio>
#include <cmath>
#include <cstring>
#include <ctime>
#include <iostream>
#include <algorithm>
#include <set>
#include <vector>
#include <sstream>
#include <queue>
#include <typeinfo>
#include <fstream>
#include <map>
#include <stack>
typedef long long ll;
using namespace std;
//freopen("D.in","r",stdin);
//freopen("D.out","w",stdout);
#define sspeed ios_base::sync_with_stdio(0);cin.tie(0)
#define maxn 2000000 + 500
#define mod 10007
#define eps 1e-9
int Num;
char CH[20];
//const int inf=0x7fffffff;   //нчоч╢С
const int inf=0x3f3f3f3f;
/*

inline void P(int x)
{
    Num=0;if(!x){putchar('0');puts("");return;}
    while(x>0)CH[++Num]=x%10,x/=10;
    while(Num)putchar(CH[Num--]+48);
    puts("");
}
*/
//**************************************************************************************
long long n , k , x;

struct tree
{
    int L , R  ;
    ll sum;
};

tree a[maxn * 4];
ll d[maxn];
void build(int x,int l,int r)
{
    a[x].L = l,a[x].R = r;
    if(l == r)
    {
        a[x].sum = d[l];
        return;
    }
    else
    {
        int mid = (l+r)>>1;
        build(x<<1,l,mid);
        build(x<<1|1,mid+1,r);
        a[x].sum = a[x<<1].sum | a[x<<1|1].sum;
    }
}

ll query(int o,int QL,int QR)
{
    int L = a[o].L , R = a[o].R;
    if (QL <= L && R <= QR) return a[o].sum;
    else
    {
        int mid = (L+R)>>1;
        ll res = 0;
        if (QL <= mid) res |= query(2*o,QL,QR);
        if (QR > mid) res |= query(2*o+1,QL,QR);
        return res;
    }
}
int main()
{
    cin>>n>>k>>x;

    for(int i = 1 ; i <= n ; ++ i)
    {
        scanf("%I64d",&d[i]);
    }
    build( 1 , 0 , n+1);
    ll ans = 0;
    ll temp = 1;
    for(int i=1;i<=k;i++)
        temp *= x;
    for(int i = 1 ; i <= n ; ++ i)
    {
        ans = max( ans , (d[i]*temp) | query(1,0,i-1) | query(1,i+1,n+1));
    }
    cout<<ans<<endl;
}